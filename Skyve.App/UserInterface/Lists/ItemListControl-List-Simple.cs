﻿using Skyve.Domain;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.UserInterface.Lists;

public partial class ItemListControl
{
	public partial class Simple : ItemListControl
	{
		public Simple(SkyvePage page) : base(page)
		{
			GridItemSize = new Size(200, 236);
			DynamicSizing = true;
		}

		private void OnPaintItemCompactList(ItemPaintEventArgs<IPackageIdentity, Rectangles> e)
		{
			var package = e.Item.GetPackage();
			var workshopInfo = e.Item.GetWorkshopInfo();
			var isPressed = false;
			var isIncluded = e.Item.IsIncluded(out var partialIncluded) || partialIncluded;
			var isEnabled = e.Item.IsEnabled();

			var compatibilityReport = e.Item.GetCompatibilityInfo();
			var notificationType = compatibilityReport?.GetNotification();
			var hasStatus = GetStatusDescriptors(e.Item, out var statusText, out var statusIcon, out var statusColor);

			if (e.IsSelected)
			{
				e.BackColor = FormDesign.Design.GreenColor.MergeColor(FormDesign.Design.BackColor);
			}
			else if (!IsPackagePage && notificationType > NotificationType.Info)
			{
				e.BackColor = notificationType.Value.GetColor().MergeColor(FormDesign.Design.BackColor, 25);
			}
			else if (hasStatus)
			{
				e.BackColor = statusColor.MergeColor(FormDesign.Design.BackColor).MergeColor(FormDesign.Design.BackColor, 25);
			}
			else if (e.HoverState.HasFlag(HoverState.Hovered))
			{
				e.BackColor = FormDesign.Design.AccentBackColor;
			}
			else
			{
				e.BackColor = BackColor;
			}

			if (!IsPackagePage && e.HoverState.HasFlag(HoverState.Hovered) && (e.Rects.CenterRect.Contains(CursorLocation) || e.Rects.IconRect.Contains(CursorLocation)))
			{
				e.BackColor = e.BackColor.MergeColor(FormDesign.Design.ActiveColor, e.HoverState.HasFlag(HoverState.Pressed) ? 0 : 90);

				isPressed = e.HoverState.HasFlag(HoverState.Pressed);
			}

			base.OnPaintItemList(e);

			e.Graphics.SetClip(new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y - Padding.Top + 1, e.ClipRectangle.Width, e.ClipRectangle.Height + Padding.Vertical - 2));

			DrawTitleAndTagsAndVersionForList(e, package?.LocalData, workshopInfo, isPressed);
			DrawIncludedButton(e, isIncluded, partialIncluded, isEnabled, package?.LocalData, out var activeColor);

			if (workshopInfo?.Author is not null)
			{
				DrawAuthor(e, workshopInfo.Author);
			}
			else if (e.Item.IsLocal())
			{
				DrawFolderName(e, package?.LocalData);
			}

			DrawButtons(e, isPressed, package?.LocalData, workshopInfo);

			DrawCompatibilityAndStatusList(e, notificationType, statusText, statusIcon, statusColor);

			DrawTags(e, _columnSizes[Columns.Tags].X + _columnSizes[Columns.Tags].Width);

			e.Graphics.ResetClip();

			if (!isIncluded && package?.LocalData is not null && !e.HoverState.HasFlag(HoverState.Hovered))
			{
				using var brush = new SolidBrush(Color.FromArgb(85, BackColor));
				e.Graphics.FillRectangle(brush, e.ClipRectangle.InvertPad(Padding));
			}
		}

		protected override void OnPaintItemList(ItemPaintEventArgs<IPackageIdentity, Rectangles> e)
		{
			if (CompactList)
			{
				OnPaintItemCompactList(e);

				return;
			}

			var package = e.Item.GetPackage();
			var localIdentity = e.Item.GetLocalPackageIdentity();
			var workshopInfo = e.Item.GetWorkshopInfo();
			var isIncluded = e.Item.IsIncluded(out var partialIncluded) || partialIncluded;
			var isEnabled = e.Item.IsEnabled();

			var compatibilityReport = e.Item.GetCompatibilityInfo();
			var notificationType = compatibilityReport?.GetNotification();
			var hasStatus = GetStatusDescriptors(e.Item, out var statusText, out var statusIcon, out var statusColor);

			if (e.IsSelected)
			{
				e.BackColor = FormDesign.Design.GreenColor.MergeColor(FormDesign.Design.BackColor);
			}
			else if (!IsPackagePage && notificationType > NotificationType.Info)
			{
				e.BackColor = notificationType.Value.GetColor().MergeColor(FormDesign.Design.BackColor, 25);
			}
			else if (!IsPackagePage && hasStatus)
			{
				e.BackColor = statusColor.MergeColor(FormDesign.Design.BackColor).MergeColor(FormDesign.Design.BackColor, 25);
			}
			else if (e.HoverState.HasFlag(HoverState.Hovered))
			{
				e.BackColor = FormDesign.Design.AccentBackColor;
			}
			else
			{
				e.BackColor = BackColor;
			}

			if (!IsPackagePage && e.HoverState.HasFlag(HoverState.Hovered) && (e.Rects.CenterRect.Contains(CursorLocation) || e.Rects.IconRect.Contains(CursorLocation)))
			{
				e.BackColor = e.BackColor.MergeColor(FormDesign.Design.ActiveColor, e.HoverState.HasFlag(HoverState.Pressed) ? 0 : 90);
			}

			base.OnPaintItemList(e);

			DrawThumbnail(e);
			DrawTitleAndTags(e, package, localIdentity, workshopInfo);
			DrawVersionAndTags(e, package, localIdentity, workshopInfo);
			DrawIncludedButton(e, isIncluded, partialIncluded, isEnabled, package?.LocalData, out var activeColor);
			DrawCenterInfo(e, workshopInfo);
			DrawButtons(e, false, package?.LocalData, workshopInfo);

			if (!IsPackagePage)
			{
				DrawCompatibilityAndStatusList(e, notificationType, statusText, statusIcon, statusColor);
			}

			e.Graphics.ResetClip();

			if (!isEnabled && isIncluded && !IsPackagePage && !e.HoverState.HasFlag(HoverState.Hovered))
			{
				using var brush = new SolidBrush(Color.FromArgb(85, BackColor));
				e.Graphics.FillRectangle(brush, e.ClipRectangle.InvertPad(Padding));
			}
		}

		private void DrawCenterInfo(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, IWorkshopInfo? workshopInfo)
		{
			using var font = UI.Font(8.25F);
			using var fontUnderline = UI.Font(7.5F, FontStyle.Underline);
			using var brush = new SolidBrush(e.BackColor.GetTextColor());

			var itemHeight = (int)e.Graphics.Measure(" ", font).Height;
			var rect = new Rectangle(e.Rects.TextRect.Right + itemHeight, e.Rects.TextRect.Y, e.ClipRectangle.Width * 2 / 10, e.Rects.IconRect.Height);
			var author = workshopInfo?.Author;

			if (author?.Name is not null and not "")
			{
				DrawAuthor(e, workshopInfo, rect.Pad(-itemHeight,0,0,0));
				
				rect.Y += itemHeight + Padding.Top;
			}

			if (workshopInfo is not null)
			{
				using var subsIcon = IconManager.GetIcon("I_People", itemHeight).Color(brush.Color);
				e.Graphics.DrawImage(subsIcon, new Rectangle(e.Rects.TextRect.Right, rect.Y, 0, rect.Height).Align(subsIcon.Size, ContentAlignment.TopLeft));
				e.Graphics.DrawString(workshopInfo.Subscribers.ToString(), font, brush, rect);

				rect.Y += itemHeight + Padding.Top;

				using var voteIcon = IconManager.GetIcon("I_Vote", itemHeight).Color(brush.Color);
				e.Graphics.DrawImage(voteIcon, new Rectangle(e.Rects.TextRect.Right, rect.Y, 0, rect.Height).Align(voteIcon.Size, ContentAlignment.TopLeft));
				e.Graphics.DrawString(workshopInfo.VoteCount.ToString(), font, brush, rect);

				rect.Y += itemHeight + Padding.Top;
			}
		}

		private void DrawCompatibilityAndStatusList(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, NotificationType? notificationType, string? statusText, DynamicIcon? statusIcon, Color statusColor)
		{
			var height = CompactList ? ((int)(24 * UI.FontScale) - 4) : (Math.Max(e.Rects.SteamRect.Y, e.Rects.FolderRect.Y) - e.ClipRectangle.Top - Padding.Vertical);

			if (notificationType > NotificationType.Info)
			{
				var point = CompactList
					? new Point(_columnSizes[Columns.Status].X, e.ClipRectangle.Y + ((e.ClipRectangle.Height - height) / 2))
					: new Point(e.ClipRectangle.Right - Padding.Horizontal, e.ClipRectangle.Top + Padding.Top);

				e.Rects.CompatibilityRect = e.Graphics.DrawLargeLabel(
					point,
					LocaleCR.Get($"{notificationType}"),
					"I_CompatibilityReport",
					notificationType.Value.GetColor(),
					CompactList ? ContentAlignment.TopLeft : ContentAlignment.TopRight,
					Padding,
					height,
					CursorLocation,
					CompactList);
			}

			if (statusText is not null && statusIcon is not null)
			{
				var point = CompactList
					? new Point(notificationType > NotificationType.Info ? (e.Rects.CompatibilityRect.Right + Padding.Left) : _columnSizes[Columns.Status].X, e.ClipRectangle.Y + ((e.ClipRectangle.Height - height) / 2))
					: new Point(notificationType > NotificationType.Info ? (e.Rects.CompatibilityRect.X - Padding.Left) : e.ClipRectangle.Right - Padding.Horizontal, e.ClipRectangle.Top + Padding.Top);

				e.Rects.DownloadStatusRect = e.Graphics.DrawLargeLabel(
					point,
					notificationType > NotificationType.Info ? "" : statusText,
					statusIcon,
					statusColor,
					CompactList ? ContentAlignment.TopLeft : ContentAlignment.TopRight,
					Padding,
					height,
					CursorLocation,
					CompactList);
			}

			if (CompactList && Math.Max(e.Rects.CompatibilityRect.Right, e.Rects.DownloadStatusRect.Right) > (_columnSizes[Columns.Status].X + _columnSizes[Columns.Status].Width))
			{
				DrawSeam(e, _columnSizes[Columns.Status].X + _columnSizes[Columns.Status].Width);
			}
		}

		private enum Columns
		{
			PackageName,
			Version,
			UpdateTime,
			Author,
			Tags,
			Status,
			Buttons
		}

		private readonly Dictionary<Columns, (int X, int Width)> _columnSizes = new();

		protected override void DrawHeader(PaintEventArgs e)
		{
			var headers = new (string text, int width)[]
			{
			(Locale.Package, 0),
			(Locale.Version, 65),
			(Locale.UpdateTime, 120),
			(Locale.Author, 120),
			(Locale.IDAndTags, 0),
			(Locale.Status, 160),
			("", 80)
			};

			var remainingWidth = Width - (int)(headers.Sum(x => x.width) * UI.FontScale);
			var autoColumns = headers.Count(x => x.width == 0);
			var xPos = 0;

			using var font = UI.Font(7.5F, FontStyle.Bold);
			using var brush = new SolidBrush(FormDesign.Design.LabelColor);
			using var lineBrush = new SolidBrush(FormDesign.Design.AccentColor);

			e.Graphics.Clear(FormDesign.Design.AccentBackColor);

			e.Graphics.FillRectangle(lineBrush, new Rectangle(0, StartHeight - 2, Width, 2));

			for (var i = 0; i < headers.Length; i++)
			{
				var header = headers[i];

				var width = header.width == 0 ? (remainingWidth / autoColumns) : (int)(header.width * UI.FontScale);

				e.Graphics.DrawString(header.text.ToUpper(), font, brush, new Rectangle(xPos, 1, width, StartHeight).Pad(Padding).AlignToFontSize(font, ContentAlignment.MiddleLeft), new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });

				_columnSizes[(Columns)i] = (xPos, width);

				xPos += width;
			}
		}

		protected override Rectangles GenerateRectangles(IPackageIdentity item, Rectangle rectangle)
		{
			if (GridView)
			{
				return GenerateGridRectangles(item, rectangle);
			}
			else
			{
				return GenerateListRectangles(item, rectangle);
			}
		}

		private Rectangles GenerateListRectangles(IPackageIdentity item, Rectangle rectangle)
		{
			rectangle = rectangle.Pad(Padding.Left, 0, Padding.Right, 0);

			var rects = new Rectangles(item)
			{
				IconRect = CompactList ? default : rectangle.Align(new Size(rectangle.Height - Padding.Vertical, rectangle.Height - Padding.Vertical), ContentAlignment.MiddleLeft)
			};

			var includedSize = 28;

			if (_settings.UserSettings.AdvancedIncludeEnable && item.GetPackage()?.IsCodeMod == true)
			{
				rects.EnabledRect = rects.IncludedRect = rectangle.Pad(Padding).Align(new Size((int)(includedSize * UI.FontScale), CompactList ? (int)(22 * UI.FontScale) : (rects.IconRect.Height / 2)), ContentAlignment.MiddleLeft);

				if (CompactList)
				{
					rects.EnabledRect.X += rects.EnabledRect.Width;
				}
				else
				{
					rects.IncludedRect.Y -= rects.IncludedRect.Height / 2;
					rects.EnabledRect.Y += rects.EnabledRect.Height / 2;
				}
			}
			else
			{
				rects.IncludedRect = rectangle.Pad(Padding).Align(UI.Scale(new Size(includedSize, CompactList ? 22 : includedSize), UI.FontScale), ContentAlignment.MiddleLeft);
			}

			if (CompactList)
			{
				rects.TextRect = new Rectangle(_columnSizes[Columns.PackageName].X, rectangle.Y, _columnSizes[Columns.PackageName].Width, rectangle.Height).Pad(Math.Max(rects.IncludedRect.Right, rects.EnabledRect.Right) + Padding.Horizontal, 0, 0, 0);
			}
			else
			{
				rects.IconRect.X += rects.IncludedRect.Right + Padding.Horizontal;

				rects.TextRect = new Rectangle(rects.IconRect.Right + Padding.Left * 2, rectangle.Y + Padding.Top, rectangle.Width * 6 / 10 - rects.IconRect.Right - Padding.Left, 0).AlignToFontSize(UI.Font(CompactList ? 8.25F : 9.75F, FontStyle.Bold), ContentAlignment.TopLeft);
			}

			rects.CenterRect = rects.TextRect.Pad(-Padding.Horizontal, 0, 0, 0);

			return rects;
		}
	}
}
