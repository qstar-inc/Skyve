﻿using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.UserInterface.Dashboard;
public abstract class IDashboardItem : SlickImageControl
{
	private readonly List<Rectangle> _sections = new();

	public event EventHandler? ResizeRequested;

	public string Key { get; }
	public bool MoveInProgress { get; internal set; }
	public bool ResizeInProgress { get; internal set; }

	public IDashboardItem()
	{
		Key = GetType().Name;
	}

	protected delegate void DrawingDelegate(PaintEventArgs e, bool applyDrawing, ref int preferredHeight);
	protected abstract DrawingDelegate GetDrawingMethod(int width);

	public virtual bool MoveAreaContains(Point point)
	{
		if (_sections.Count > 0)
		{
			return _sections.Any(x => x.Contains(point));
		}

		return point.Y < Padding.Top *3/2;
	}

	public int CalculateHeight(int width, Graphics graphics)
	{
		var clip = ClientRectangle;

		clip.Width = width;

		using var pe = new PaintEventArgs(graphics, clip.Pad(Padding));

		graphics.SetClip(pe.ClipRectangle);

		var height = pe.ClipRectangle.Y;

		_sections.Clear();

		try
		{
			GetDrawingMethod(pe.ClipRectangle.Width).Invoke(pe, false, ref height);
		}
		catch { }

		return height;
	}

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(12, 12, 0, 0), UI.FontScale);
		Margin = UI.Scale(new Padding(8), UI.FontScale);
	}

	protected void OnResizeRequested()
	{
		ResizeRequested?.Invoke(this, EventArgs.Empty);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		if (MoveInProgress || ResizeInProgress)
		{
			var border = (int)(10 * UI.FontScale);
			var rect = ClientRectangle.Pad((int)(1.5 * UI.FontScale)).Pad(Padding);

			using var brush = new SolidBrush(FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.Type.If(FormDesignType.Dark, 8, -8)));
			e.Graphics.FillRoundedRectangle(brush, rect, border);

			using var pen = new Pen(FormDesign.Design.AccentColor, (float)(1.5 * UI.FontScale));
			e.Graphics.DrawRoundedRectangle(pen, rect, border);

			return;
		}

		try
		{
			using var pe = new PaintEventArgs(e.Graphics, ClientRectangle.Pad(Padding));

			e.Graphics.SetClip(pe.ClipRectangle);

			var height = pe.ClipRectangle.Y;

			GetDrawingMethod(pe.ClipRectangle.Width).Invoke(pe, true, ref height);
		}
		catch { }

		base.OnPaint(e);

		var dragRect = ClientRectangle.Align(UI.Scale(new Size(16, 16), UI.UIScale), ContentAlignment.BottomRight);

		using var dotBrush = new SolidBrush(HoverState.HasFlag(HoverState.Hovered) ? Color.FromArgb(150, FormDesign.Design.ActiveColor) : Color.FromArgb(50, FormDesign.Design.AccentColor));

		for (var i = 3; i > 0; i--)
		{
			var y = dragRect.Y + ((3 - i) * dragRect.Height / 3.75F);

			for (var j = i; j > 0; j--)
			{
				var x = dragRect.X + ((j - 1) * dragRect.Width / 3.75F);

				e.Graphics.FillEllipse(dotBrush, x, y, dragRect.Width / 5F, dragRect.Width / 5F);
			}
		}

		if (HoverState.HasFlag(HoverState.Hovered))
		{
			using var grabberBrush = new SolidBrush(Color.FromArgb(25, FormDesign.Design.MenuForeColor));

			foreach (var rectangle in _sections)
			{
				e.Graphics.FillRoundedRectangle(grabberBrush, rectangle.Pad(2), Padding.Left/*, botLeft: false, botRight: false*/);
			}
		}
	}

	protected void DrawSection(PaintEventArgs e, bool applyDrawing, Rectangle rectangle, string text, DynamicIcon dynamicIcon, out Color fore, ref int preferredHeight, Color? tintColor = null)
	{
		var hoverState = rectangle.Contains(CursorLocation) ? (HoverState & ~HoverState.Focused) : HoverState.Normal;

		Color back;

		//if (hoverState.HasFlag(HoverState.Pressed))
		//{
		//	fore = ColorStyle.Active.GetBackColor().Tint(tintColor?.GetHue());
		//	back = tintColor == null ? ColorStyle.Active.GetColor() : ColorStyle.Active.GetColor().Tint(tintColor.Value.GetHue()).MergeColor(tintColor.Value);
		//}
		//else if (hoverState.HasFlag(HoverState.Hovered))
		//{
		//	fore = FormDesign.Design.MenuForeColor.Tint(Lum: FormDesign.Design.Type == FormDesignType.Light ? -3 : 3);
		//	back = FormDesign.Design.MenuColor.Tint(Lum: FormDesign.Design.Type == FormDesignType.Light ? -3 : 3);
		//}
		//else
		{
			fore = FormDesign.Design.MenuForeColor;
			back = FormDesign.Design.MenuColor;
		}

		if (applyDrawing)
		{
			if (tintColor != null)
			{
				//	if (hoverState.HasFlag(HoverState.Pressed))
				//	{
				//		back = tintColor.Value;
				//	}
				//	else
				//	{
				back = back.MergeColor(tintColor.Value, 25);
				//}

				fore = Color.FromArgb(220, back.GetTextColor());
			}

			//if (!hoverState.HasFlag(HoverState.Pressed) && FormDesign.Design.Type == FormDesignType.Light)
			//{
			//	back = back.Tint(Lum: 1.5F);
			//}

			using var gradient = rectangle.Gradient(back, /*hoverState.HasFlag(HoverState.Pressed) ? 1F :*/ 0.5F);
			e.Graphics.FillRoundedRectangle(gradient, rectangle.Pad(2), Padding.Left);
		}

		if (!string.IsNullOrEmpty(text))
		{
			using var font = UI.Font(9.75F, FontStyle.Bold);
			using var icon = dynamicIcon?.Get(font.Height + Margin.Top);
			var iconRectangle = new Rectangle(Margin.Left + rectangle.X, rectangle.Y, icon?.Width ?? 0, icon?.Height ?? 0);
			var titleHeight = Math.Max(icon?.Height ?? 0, (int)e.Graphics.Measure(text, font, rectangle.Right - Margin.Horizontal - iconRectangle.Right).Height);

			iconRectangle.Y += Margin.Top + ((titleHeight - icon?.Height ?? 0) / 2);

			if (applyDrawing)
			{
				if (icon is not null)
				{
					try
					{
						e.Graphics.DrawImage(icon.Color(fore), iconRectangle);
					}
					catch { }
				}

				using var brush = new SolidBrush(fore);
				e.Graphics.DrawString(text, font, brush, new Rectangle(iconRectangle.Right + Margin.Left, Margin.Top + rectangle.Y, rectangle.Right - Margin.Horizontal - iconRectangle.Right, titleHeight), new StringFormat { LineAlignment = StringAlignment.Center });
			}
			else
			{
				_sections.Add(new(rectangle.X, rectangle.Y, rectangle.Width, titleHeight + (Margin.Top * 2)));
			}

			preferredHeight += titleHeight + (Margin.Top * 2);
		}
	}

	protected void DrawLoadingSection(PaintEventArgs e, bool applyDrawing, string text, DynamicIcon icon, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, e.ClipRectangle, text, icon, out _, ref preferredHeight);

		DrawLoader(e.Graphics, e.ClipRectangle.Pad(Margin).Align(UI.Scale(new Size(32, 32), UI.FontScale), ContentAlignment.BottomCenter));

		preferredHeight += (int)(32 * UI.FontScale) + Margin.Vertical;
	}

	protected void DrawSquareButton(PaintEventArgs e, bool applyDrawing, ref int preferredHeight, ButtonDrawArgs buttonArgs)
	{
		using var icon = buttonArgs.Icon.Get(Math.Min(buttonArgs.Rectangle.Width, buttonArgs.Rectangle.Height) *3/ 5);

		buttonArgs.Image = icon;
		buttonArgs.BorderRadius = Padding.Left;

		DrawButtonInternal(e, applyDrawing, ref preferredHeight, true, buttonArgs);
	}

	protected void DrawButton(PaintEventArgs e, bool applyDrawing, ref int preferredHeight, ButtonDrawArgs buttonArgs)
	{
		using var icon = buttonArgs.Icon.Default;

		buttonArgs.Image = icon;

		DrawButtonInternal(e, applyDrawing, ref preferredHeight, false, buttonArgs);
	}

	private void DrawButtonInternal(PaintEventArgs e, bool applyDrawing, ref int preferredHeight, bool square, ButtonDrawArgs buttonArgs)
	{
		buttonArgs.Font ??= Font;
		buttonArgs.Padding = Margin;
		buttonArgs.Rectangle = new Rectangle(buttonArgs.Rectangle.X, preferredHeight, buttonArgs.Rectangle.Width, square ? buttonArgs.Rectangle.Height : SlickButton.GetSize(e.Graphics, buttonArgs.Image, buttonArgs.Text, buttonArgs.Font, buttonArgs.Padding, buttonArgs.Rectangle.Width).Height);
		buttonArgs.HoverState = buttonArgs.Rectangle.Contains(CursorLocation) ? (HoverState & ~HoverState.Focused) : HoverState.Normal;

		preferredHeight += buttonArgs.Rectangle.Height + Margin.Bottom;

		if (!applyDrawing)
		{
			return;
		}

		SlickButton.Draw(e, buttonArgs);
	}
}
