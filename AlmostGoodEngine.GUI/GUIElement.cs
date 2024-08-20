using AlmostGoodEngine.GUI.Utils;
using AlmostGoodEngine.Inputs;
using AngleSharp.Css.Dom;
using Apos.Shapes;
using ExCSS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MColor = Microsoft.Xna.Framework.Color;

namespace AlmostGoodEngine.GUI
{
    public class GUIElement
    {
        #region Overall
        /// <summary>
        /// The css id of this element
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The css classes of this element
        /// </summary>
        public List<string> Classes { get; set; } = [];

        /// <summary>
        /// The style rules of the element
        /// </summary>
        public GUIStyle Style { get; set; }

        /// <summary>
        /// The docking of the element
        /// </summary>
        public GUIDock Dock { get; set; } = GUIDock.None;

        /// <summary>
        /// Element's parent
        /// </summary>
        public GUIElement Parent { get; set; }

        /// <summary>
        /// Element's children
        /// </summary>
        public List<GUIElement> Children { get; set; } = [];
        #endregion

        #region State

        /// <summary>
        /// True if the element is hovered by the mouse
        /// </summary>
        public bool IsHovered { get; protected set; }

        /// <summary>
        /// True if the user clicked on the element
        /// </summary>
        public bool IsPressed { get; protected set; }

        /// <summary>
        /// True if the mouse left button down on the element
        /// </summary>
        public bool IsDown { get; protected set; }

        #endregion

        #region Actions

        /// <summary>
        /// The callback used when the mouse is hovering the element
        /// </summary>
        public Action OnHover { get; set; }

        /// <summary>
        /// The callback used when the mouse left button is down on the element
        /// </summary>
        public Action OnDown { get; set; }

        /// <summary>
        /// The callback used when the mouse left button clicked the element
        /// </summary>
        public Action OnPressed { get; set; }

        #endregion

        #region Global element's settings
        /// <summary>
        /// Get the X coordinate of this element
        /// </summary>
        public int X
        {
            get
            {
                int x = 0;
                if (Dock != GUIDock.None)
                {
                    switch (Dock)
                    {
                        case GUIDock.Left:
                        case GUIDock.Top:
                        case GUIDock.Bottom:
                        case GUIDock.FillHorizontaly:
                        case GUIDock.Fill:
                            x = 0;
                            break;
                        case GUIDock.Right:
                            x = Parent.X;
                            break;
                        case GUIDock.FillVerticaly:
                            x = Parent.X + Parent.Width / 2 - Width / 2;
                            break;
                    }
                }

                if (Parent != null)
                {
                    x = Parent.X;
                }
                x += Style.Left;
                x -= Style.Right;
                x += Style.MarginLeft;
                x -= Style.MarginRight;
                return x;
            }
        }

        /// <summary>
        /// Get the Y coordinate of this element
        /// </summary>
        public int Y
        {
            get
            {
                int y = 0;
                if (Parent != null)
                {
                    y = Parent.Y;
                }
                y += Style.Top;
                y -= Style.Bottom;
                y += Style.MarginLeft;
                y -= Style.MarginRight;
                return y;
            }
        }

        /// <summary>
        /// The width of the element (in px)
        /// </summary>
        public int Width
        {
            get
            {
                return Style.GetWidth(this);
            }
        }

        /// <summary>
        /// The height of the element (in px)
        /// </summary>
        public int Height
        {
            get
            {
                return Style.GetHeight(this);
            }
        }

        /// <summary>
        /// The border size of the element (in px)
        /// </summary>
        public int Border
        {
            get
            {
                return Style.Border;
            }
        }

        /// <summary>
        /// The border radius of the element (in px)
        /// </summary>
        public int BorderRadius
        {
            get
            {
                return Style.BorderRadius;
            }
        }
        #endregion

        #region Parameters
        private bool doingTransition = false;
        private bool transitionFinished = true;
        private float transitionTimer = 0f;

        private bool wasHovered = false;
        private bool wasDown = false;
        #endregion

        #region Colors
        /// <summary>
        /// The background color of the element
        /// </summary>
        public MColor BackgroundColor
        {
            get
            {
                return Style.BackgroundColor;
            }
        }

        /// <summary>
        /// The border color of the element
        /// </summary>
        public MColor BorderColor
        {
            get
            {
                return Style.BorderColor;
            }
        }

        /// <summary>
        /// The scissor rectangle used by the element (to cut children)
        /// </summary>
        public Rectangle ScissorRectangle
        {
            get => new(X, Y, Width, Height);
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public GUIElement()
        {
            Style = new();
        }

        /// <summary>
        /// Update the element state
        /// </summary>
        /// <param name="delta"></param>
        public virtual void Update(float delta)
        {
            if (!Style.Events)
            {
                return;
            }

            IsHovered = Input.Mouse.X >= X && Input.Mouse.Y >= Y && Input.Mouse.X < X + Width && Input.Mouse.Y < Y + Height;
            IsDown = IsHovered && Input.Mouse.IsLeftButtonDown();
            IsPressed = IsHovered && Input.Mouse.IsLeftButtonPressed();

            if (doingTransition)
            {
                if (transitionTimer >= Style.TransitionDuration)
                {
                    transitionTimer = Style.TransitionDuration;
                    transitionFinished = true;
                    doingTransition = false;
                }
                else
                {
                    transitionTimer += delta;
                }
            }

            if (IsHovered)
            {
                OnHover?.Invoke();
            }

            if (IsDown)
            {
                OnDown?.Invoke();
            }

            if (IsPressed)
            {
                OnPressed?.Invoke();
            }

            // Update previous state
            wasDown = IsDown;
            wasHovered = IsHovered;
        }

        /// <summary>
        /// Element rendering
        /// </summary>
        /// <param name="shapeBatch"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="delta"></param>
        public virtual void Draw(ShapeBatch shapeBatch, SpriteBatch spriteBatch, float delta)
        {
            // Do not render anything if any axis does not have a size
            if (Width == 0 || Height == 0)
            {
                return;
            }

            // Background is transparent and no border or transparent border
            if (BackgroundColor == MColor.Transparent && (Border == 0 || BorderColor == MColor.Transparent))
            {
                return;
            }

            // Draw the element
            if (Style.Border > 0)
            {
                shapeBatch.DrawRectangle(
                    new Vector2(X, Y),
                    new Vector2(Width, Height),
                    BackgroundColor,
                    BorderColor,
                    Border,
                    BorderRadius);
            }
            else
            {
                shapeBatch.FillRectangle(
                    new Vector2(X, Y),
                    new Vector2(Width, Height),
                    BackgroundColor,
                    BorderRadius);
            }
        }

        /// <summary>
        /// Find a child of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetElement<T>() where T : GUIElement
        {
            foreach (var child in Children)
            {
                if (child.GetType() == typeof(T))
                {
                    return (T)child;
                }
            }

            return default;
        }

        /// <summary>
        /// Find children of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetElements<T>() where T : GUIElement
        {
            List<T> elements = [];

            foreach (var child in Children)
            {
                if (child.GetType() == typeof(T))
                {
                    elements.Add((T)child);
                }
            }

            return elements;
        }

        /// <summary>
        /// Find a child using the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GUIElement GetElementById(string id)
        {
            foreach (var child in Children)
            {
                if (child.Id == id)
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a child using the given class name
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public GUIElement GetElementByClass(string className)
        {
            foreach (var child in Children)
            {
                if (child.Classes.Contains(className))
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// Find children using the given class name
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public List<GUIElement> GetElementsByClass(string className)
        {
            List<GUIElement> elements = [];

            foreach (var child in Children)
            {
                if (child.Classes.Contains(className))
                {
                    elements.Add(child);
                }

                if (child.Children.Count > 0)
                {
                    elements.AddRange(child.GetElementsByClass(className));
                }
            }

            return elements;
        }

        /// <summary>
        /// Apply the given style on this element
        /// </summary>
        /// <param name="style"></param>
        public void ApplyStyle(ICssStyleDeclaration style)
        {
            ApplyStyleOn(Style, style);
        }

        /// <summary>
        /// Hydrate the given style with the given stylesheet
        /// </summary>
        /// <param name="style"></param>
        /// <param name="properties"></param>
        private void ApplyStyleOn(GUIStyle style, ICssStyleDeclaration properties, bool replaceIfExists = false)
        {
            // No style or no properties
            if (properties == null)
            {
                return;
            }

            // Background color
            if (IsValid(properties.GetBackgroundColor()))
            {
                style.BackgroundColor = StylesheetHelper.FromCssString(properties.GetBackgroundColor());
            }

            // Border color
            if (IsValid(properties.GetBorderColor()))
            {
                style.BorderColor = StylesheetHelper.FromCssString(properties.GetBorderColor());
            }

            // Text color
            if (IsValid(properties.GetColor()))
            {
                style.TextColor = StylesheetHelper.FromCssString(properties.GetColor());
            }

            // Width
            if (IsValid(properties.GetWidth()))
            {
                if (IsPercent(properties.GetWidth()))
                {
                    if (properties.GetWidth() == "100%")
                    {
                        style.FullWidth = true;
                    }
                    else
                    {
                        style.WidthPercent = true;
                    }
                }
                else
                {
                    style.FullWidth = false;
                    style.WidthPercent = false;
                }
                if (IsAuto(properties.GetWidth()))
                {
                    style.AutoWidth = true;
                }
                else
                {
                    style.AutoWidth = false;
                }

                var width = properties.GetWidth();
                style.Width = StylesheetHelper.FromCssToSize(properties.GetWidth());
            }

            // Height
            if (IsValid(properties.GetHeight()))
            {
                if (IsPercent(properties.GetHeight()))
                {
                    if (properties.GetHeight() == "100%")
                    {
                        style.FullHeight = true;
                    }
                    else
                    {
                        style.HeightPercent = true;
                    }
                }
                else
                {
                    style.FullHeight = false;
                    style.HeightPercent = false;
                }
                if (IsAuto(properties.GetHeight()))
                {
                    style.AutoHeight = true;
                }
                else
                {
                    style.AutoHeight = false;
                }

                style.Height = StylesheetHelper.FromCssToSize(properties.GetHeight());
            }

            // Left
            if (IsValid(properties.GetLeft()))
            {
                if (IsPercent(properties.GetLeft()))
                {
                    style.LeftPercent = true;
                }

                style.Left = StylesheetHelper.FromCssToSize(properties.GetLeft());
            }

            // Right
            if (IsValid(properties.GetRight()))
            {
                if (IsPercent(properties.GetRight()))
                {
                    style.RightPercent = true;
                }

                style.Right = StylesheetHelper.FromCssToSize(properties.GetRight());
            }

            // Top
            if (IsValid(properties.GetTop()))
            {
                if (IsPercent(properties.GetTop()))
                {
                    style.TopPercent = true;
                }

                style.Top = StylesheetHelper.FromCssToSize(properties.GetTop());
            }

            // Bottom
            if (IsValid(properties.GetBottom()))
            {
                if (IsPercent(properties.GetBottom()))
                {
                    style.BottomPercent = true;
                }

                style.Bottom = StylesheetHelper.FromCssToSize(properties.GetBottom());
            }

            // Margin left
            if (IsValid(properties.GetMarginLeft()))
            {
                if (IsPercent(properties.GetMarginLeft()))
                {
                    style.MarginLeftPercent = true;
                }

                style.MarginLeft = StylesheetHelper.FromCssToSize(properties.GetMarginLeft());
            }

            // Margin right
            if (IsValid(properties.GetMarginRight()))
            {
                if (IsPercent(properties.GetMarginRight()))
                {
                    style.MarginRightPercent = true;
                }

                style.MarginRight = StylesheetHelper.FromCssToSize(properties.GetMarginRight());
            }

            // Margin top
            if (IsValid(properties.GetMarginTop()))
            {
                if (IsPercent(properties.GetMarginTop()))
                {
                    style.MarginTopPercent = true;
                }

                style.MarginTop = StylesheetHelper.FromCssToSize(properties.GetMarginTop());
            }

            // Margin bottom
            if (IsValid(properties.GetMarginBottom()))
            {
                if (IsPercent(properties.GetMarginBottom()))
                {
                    style.MarginBottomPercent = true;
                }

                style.MarginBottom = StylesheetHelper.FromCssToSize(properties.GetMarginBottom());
            }

            // Padding left
            if (IsValid(properties.GetPaddingLeft()))
            {
                if (IsPercent(properties.GetPaddingLeft()))
                {
                    style.PaddingLeftPercent = true;
                }

                style.PaddingLeft = StylesheetHelper.FromCssToSize(properties.GetPaddingLeft());
            }

            // Padding right
            if (IsValid(properties.GetPaddingRight()))
            {
                if (IsPercent(properties.GetPaddingRight()))
                {
                    style.PaddingRightPercent = true;
                }

                style.PaddingRight = StylesheetHelper.FromCssToSize(properties.GetPaddingRight());
            }

            // Padding top
            if (IsValid(properties.GetPaddingTop()))
            {
                if (IsPercent(properties.GetPaddingTop()))
                {
                    style.PaddingTopPercent = true;
                }

                style.PaddingTop = StylesheetHelper.FromCssToSize(properties.GetPaddingTop());
            }

            // Padding bottom
            if (IsValid(properties.GetPaddingBottom()))
            {
                if (IsPercent(properties.GetPaddingBottom()))
                {
                    style.PaddingBottomPercent = true;
                }

                style.PaddingBottom = StylesheetHelper.FromCssToSize(properties.GetPaddingBottom());
            }

            // Borders
            if (IsValid(properties.GetBorder()))
            {
                int left = StylesheetHelper.FromCssToSize(properties.GetBorderLeftWidth());
                int right = StylesheetHelper.FromCssToSize(properties.GetBorderRightWidth());
                int bottom = StylesheetHelper.FromCssToSize(properties.GetBorderBottomWidth());
                int top = StylesheetHelper.FromCssToSize(properties.GetBorderTopWidth());

                int max = Math.Max(left, right);
                max = Math.Max(max, top);
                max = Math.Max(max, bottom);

                style.Border = max;
            }
            else if (IsValid(properties.GetBorderWidth()))
            {
                style.Border = StylesheetHelper.FromCssToSize(properties.GetBorderWidth());
            }

            // Border radius
            if (IsValid(properties.GetBorderRadius()))
            {
                style.BorderRadius = StylesheetHelper.FromCssToSize(properties.GetBorderRadius());
            }

            // Transition
            if (IsValid(properties.GetTransitionDuration()))
            {
                style.TransitionDuration = StylesheetHelper.FromCssToSeconds(properties.GetTransitionDuration());
            }

            // Opacity
            if (IsValid(properties.GetOpacity()))
            {
                style.Opacity = float.Parse(properties.GetOpacity());
            }

            // Font family
            if (IsValid(properties.GetFontFamily()))
            {
                GUIManager.LoadFont("Content/Fonts/" + properties.GetFontFamily());
            }

            // Font size
            if (IsValid(properties.GetFontSize()))
            {
                style.FontSize = StylesheetHelper.FromCssToSize(properties.GetFontSize());
                style.Font = GUIManager.GetFont(style.FontSize);
            }

            // Font horizontal align
            if (IsValid(properties.GetTextAlign()))
            {
                switch (properties.GetTextAlign())
                {
                    case "left":
                        style.HAlign = GUIHAlign.Left;
                        break;
                    case "center":
                        style.HAlign = GUIHAlign.Center;
                        break;
                    case "right":
                        style.HAlign = GUIHAlign.Right;
                        break;
                }
            }

            // Text vertical align
            if (IsValid(properties.GetVerticalAlign()))
            {
                switch (properties.GetVerticalAlign())
                {
                    case "top":
                        style.VAlign = GUIVAlign.Top;
                        break;
                    case "middle":
                        style.VAlign = GUIVAlign.Middle;
                        break;
                    case "bottom":
                        style.VAlign = GUIVAlign.Bottom;
                        break;
                }
            }

            // Font content
            if (IsValid(properties.GetContent()))
            {
                style.Content = properties.GetContent().Replace("\"", "");
            }

            // Events
            if (IsValid(properties.GetPointerEvents()))
            {
                switch (properties.GetPointerEvents())
                {
                    case "all":
                    case "fill":
                        style.Events = true;
                        break;
                    case "none":
                        style.Events = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Return true if the given css string is a calc
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        private static bool IsCalc(string css)
        {
            return css.StartsWith("calc(") && css.EndsWith(")");
        }

        /// <summary>
        /// Return true if the given css string is in percent
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        private static bool IsPercent(string css)
        {
            return css.EndsWith("%") || css.EndsWith("vw") || css.EndsWith("vh");
        }

        /// <summary>
        /// Return true if the given css string is auto
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        private static bool IsAuto(string css)
        {
            return css.EndsWith("auto");
        }

        /// <summary>
        /// Return true if the given css string is valid
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        private static bool IsValid(string css)
        {
            return !string.IsNullOrWhiteSpace(css);
        }
    }
}
