﻿using Skyve.Systems.Compatibility.Domain.Api;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Skyve.App.UserInterface.Lists;

public partial class ItemListControl
{
	public partial class Complex : ItemListControl
	{
		protected override void OnPaintItemGrid(ItemPaintEventArgs<IPackageIdentity, Rectangles> e)
		{
			var package = e.Item.GetPackage();
			var localIdentity = e.Item.GetLocalPackageIdentity();
			var workshopInfo = e.Item.GetWorkshopInfo();
			var isIncluded = e.Item.IsIncluded(out var partialIncluded) || partialIncluded;
			var isEnabled = e.Item.IsEnabled();

			if (e.IsSelected)
			{
				e.BackColor = FormDesign.Design.GreenColor.MergeColor(FormDesign.Design.BackColor);
			}

			if (!IsPackagePage && e.HoverState.HasFlag(HoverState.Hovered) && (e.Rects.CenterRect.Contains(CursorLocation) || e.Rects.IconRect.Contains(CursorLocation)))
			{
				e.BackColor = (e.IsSelected ? e.BackColor : FormDesign.Design.AccentBackColor).MergeColor(FormDesign.Design.ActiveColor, e.HoverState.HasFlag(HoverState.Pressed) ? 0 : 90);
			}

			base.OnPaintItemGrid(e);

			DrawThumbnail(e, localIdentity, workshopInfo);
			DrawTitleAndTags(e);
			DrawVersionAndDate(e, package, localIdentity, workshopInfo);
			DrawVoteAndSubscribers(e, workshopInfo);
			DrawIncludedButton(e, isIncluded, partialIncluded, isEnabled, localIdentity, out var activeColor);

			if (workshopInfo?.Author is not null)
			{
				  DrawAuthor(e, workshopInfo.Author);
			}
			else if (e.Item.IsLocal())
			{
				  DrawFolderName(e, localIdentity);
			}

			DrawDividerLine(e);

			var maxTagX = DrawButtons(e, package?.LocalData, workshopInfo);

			DrawTags(e, maxTagX);

			e.Graphics.ResetClip();

			DrawCompatibilityAndStatus(e, out var outerColor);

			if (isEnabled)
			{
				if (outerColor == default)
				{
					outerColor = Color.FromArgb(FormDesign.Design.Type == FormDesignType.Dark ? 65 : 100, activeColor);
				}

				using var pen = new Pen(outerColor, (float)(1.5 * UI.FontScale));

				e.Graphics.DrawRoundedRectangle(pen, e.ClipRectangle.InvertPad(GridPadding - new Padding((int)pen.Width)), (int)(5 * UI.FontScale));
			}
			else if (isIncluded && !IsPackagePage && _settings.UserSettings.FadeDisabledItems && !e.HoverState.HasFlag(HoverState.Hovered))
			{
				using var brush = new SolidBrush(Color.FromArgb(85, BackColor));
				e.Graphics.FillRectangle(brush, e.ClipRectangle.InvertPad(GridPadding));
			}
		}

		private void DrawCompatibilityAndStatus(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, out Color outerColor)
		{
			var compatibilityReport = e.Item.GetCompatibilityInfo();
			var notificationType = compatibilityReport?.GetNotification();
			outerColor = default;

			var height = GridView ? e.Rects.IncludedRect.Height : (e.Rects.IconRect.Bottom - Math.Max(e.Rects.TextRect.Bottom, Math.Max(e.Rects.VersionRect.Bottom, e.Rects.DateRect.Bottom)) - GridPadding.Bottom);

			if (notificationType > NotificationType.Info)
			{
				outerColor = notificationType.Value.GetColor();

				e.Rects.CompatibilityRect = e.Graphics.DrawLargeLabel(new(e.ClipRectangle.Right - GridPadding.Right, e.Rects.IconRect.Bottom), LocaleCR.Get($"{notificationType}"), "I_CompatibilityReport", outerColor, ContentAlignment.BottomRight, padding: GridPadding, height: height, cursorLocation: CursorLocation);
			}

			if (GetStatusDescriptors(e.Item, out var text, out var icon, out var color))
			{
				if (!(notificationType > NotificationType.Info))
				{
					outerColor = color;
				}

				e.Rects.DownloadStatusRect = e.Graphics.DrawLargeLabel(new(notificationType > NotificationType.Info ? (e.Rects.CompatibilityRect.X - GridPadding.Left) : (e.ClipRectangle.Right - GridPadding.Right), e.Rects.IconRect.Bottom), notificationType > NotificationType.Info ? "" : text, icon!, color, ContentAlignment.BottomRight, padding: GridPadding, height: height, cursorLocation: CursorLocation);
			}

			if (e.IsSelected && outerColor == default)
			{
				outerColor = FormDesign.Design.GreenColor;
			}
		}

		private void DrawTags(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, int maxTagX)
		{
			var startLocation = GridView
				? new Point(e.ClipRectangle.X, e.Rects.IconRect.Bottom + (GridPadding.Top * 3))
				: IsPackagePage ? new Point(e.Rects.TextRect.X - Padding.Left, e.ClipRectangle.Bottom)
				: new Point(CompactList ? _columnSizes[Columns.Tags].X : (e.ClipRectangle.X + (int)(375 * UI.UIScale)), e.ClipRectangle.Bottom - (CompactList ? 0 : Padding.Bottom));
			var tagsRect = new Rectangle(startLocation, default);

			if (GridView)
			{
				e.Graphics.SetClip(new Rectangle(tagsRect.X, tagsRect.Y, maxTagX - tagsRect.X, e.ClipRectangle.Bottom - tagsRect.Y));
			}
			else
			{
				e.Graphics.SetClip(new Rectangle(tagsRect.X, e.ClipRectangle.Y, maxTagX - tagsRect.X, e.ClipRectangle.Height));
			}

			if (!IsPackagePage && e.Item.Id > 0)
			{
				e.Rects.SteamIdRect = DrawTag(e, maxTagX, startLocation, ref tagsRect, _tagsService.CreateIdTag(e.Item.Id.ToString()), FormDesign.Design.ActiveColor.MergeColor(FormDesign.Design.BackColor));

				tagsRect.X += Padding.Left;
			}

			foreach (var item in e.Item.GetTags(IsPackagePage))
			{
				DrawTag(e, maxTagX, startLocation, ref tagsRect, item);
			}

			if (CompactList)
			{
				using var backBrush = new SolidBrush(e.BackColor);
				e.Graphics.FillRectangle(backBrush, e.ClipRectangle.Pad(_columnSizes[Columns.Tags].X + _columnSizes[Columns.Tags].Width, 0, 0, 0));

				DrawSeam(e, _columnSizes[Columns.Tags].X + _columnSizes[Columns.Tags].Width);
			}
			else if (IsPackagePage)
			{
				var seamRectangle = new Rectangle(maxTagX - (int)(40 * UI.UIScale), e.Rects.TextRect.Bottom, (int)(40 * UI.UIScale), e.ClipRectangle.Height);

				using var seamBrush = new LinearGradientBrush(seamRectangle, Color.Empty, e.BackColor, 0F);

				e.Graphics.FillRectangle(seamBrush, seamRectangle);
			}
			else
			{
				DrawSeam(e, maxTagX);
			}
		}

		private void DrawDividerLine(ItemPaintEventArgs<IPackageIdentity, Rectangles> e)
		{
			var lineRect = new Rectangle(e.ClipRectangle.X, e.Rects.IconRect.Bottom + GridPadding.Vertical, e.ClipRectangle.Width, (int)(2 * UI.FontScale));
			using var lineBrush = new LinearGradientBrush(lineRect, default, default, 0F);

			lineBrush.InterpolationColors = new ColorBlend
			{
				Colors = new[] { Color.Empty, FormDesign.Design.AccentColor, FormDesign.Design.AccentColor, Color.Empty, Color.Empty },
				Positions = new[] { 0.0f, 0.15f, 0.6f, 0.75f, 1f }
			};

			e.Graphics.FillRectangle(lineBrush, lineRect);
		}

		private Rectangle DrawTag(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, int maxTagX, Point startLocation, ref Rectangle tagsRect, ITag item, Color? color = null)
		{
			using var tagIcon = IconManager.GetSmallIcon(item.Icon);

			var padding = GridView ? GridPadding : Padding;
			var tagSize = e.Graphics.MeasureLabel(item.Value, tagIcon, large: false);
			var tagRect = e.Graphics.DrawLabel(item.Value, tagIcon, color ?? Color.FromArgb(200, FormDesign.Design.LabelColor.MergeColor(FormDesign.Design.AccentBackColor, 40)), tagsRect, GridView ? ContentAlignment.TopLeft : ContentAlignment.BottomLeft, smaller: CompactList, large: false, mousePosition: CursorLocation);

			e.Rects.TagRects[item] = tagRect;

			tagsRect.X += padding.Left + tagRect.Width;

			if (tagsRect.X + tagSize.Width + (int)(25 * UI.UIScale) > maxTagX)
			{
				if (IsPackagePage)
				{
					return tagRect;
				}

				tagsRect.X = startLocation.X;
				tagsRect.Y += (GridView ? 1 : -1) * (tagRect.Height + padding.Top);
			}

			return tagRect;
		}

		private int DrawFolderName(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, ILocalPackageIdentity? localIdentity)
		{
			if (localIdentity is null)
			{
				return 0;
			}

			if (CompactList)
			{
				e.Rects.FolderNameRect = DrawCell(e, Columns.Author, Path.GetFileName(localIdentity.Folder), "I_Folder", font: UI.Font(8.25F, FontStyle.Bold));
				return 0;
			}

			if (GridView)
			{
				var rect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Bottom + (int)(40 * UI.FontScale) + GridPadding.Bottom / 2,0,0);

				using var authorIcon = IconManager.GetSmallIcon("I_Folder");

				e.Graphics.DrawLabel(Path.GetFileNameWithoutExtension(localIdentity.FilePath), authorIcon, default, rect, ContentAlignment.TopLeft);
				
				return 0;
			}

			var padding = GridView ? GridPadding : Padding;
			var height = e.Rects.IconRect.Bottom - Math.Max(e.Rects.TextRect.Bottom, Math.Max(e.Rects.VersionRect.Bottom, e.Rects.DateRect.Bottom)) - padding.Bottom;
			var folderPoint = CompactList ? new Point(_columnSizes[Columns.Author].X, e.ClipRectangle.Y) : new Point(e.Rects.TextRect.X, e.Rects.IconRect.Bottom);

			e.Rects.FolderNameRect = e.Graphics.DrawLargeLabel(folderPoint, Path.GetFileName(localIdentity.Folder), "I_Folder", alignment: ContentAlignment.BottomLeft, padding: GridView ? GridPadding : Padding, height: height, cursorLocation: CursorLocation);

			return e.Rects.FolderNameRect.Width;
		}

		private int DrawAuthor(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, IUser author)
		{
			var padding = GridView ? GridPadding : Padding;
			var authorRect = new Rectangle(e.Rects.TextRect.X, e.Rects.IconRect.Bottom, 0, 0);
			var authorImg = author.GetUserAvatar();

			if (GridView)
			{
				authorRect.Y = e.Rects.TextRect.Bottom + (int)(40 * UI.FontScale) + GridPadding.Bottom / 2;
			}

			var height = 
				GridView ? (int)(20 * UI.FontScale) : (
				CompactList ? authorRect.Height :
				e.Rects.IconRect.Bottom - Math.Max(e.Rects.TextRect.Bottom, Math.Max(e.Rects.VersionRect.Bottom, e.Rects.DateRect.Bottom)) - padding.Bottom);

			if (CompactList)
			{
				if (authorImg is null)
				{
					authorRect = DrawCell(e, Columns.Author, author.Name, "I_Author", font: UI.Font(8.25F, FontStyle.Bold));
				}
				else
				{
					authorRect = DrawCell(e, Columns.Author, author.Name, null, font: UI.Font(8.25F, FontStyle.Bold), padding: new Padding((int)(20 * UI.FontScale), 0, 0, 0));

					e.Graphics.DrawRoundImage(authorImg, authorRect.Pad(Padding).Align(UI.Scale(new Size(18, 18), UI.FontScale), ContentAlignment.MiddleLeft));
				}
			}
			else if (authorImg is null)
			{
				using var authorIcon = IconManager.GetSmallIcon("I_Author");

				authorRect = e.Graphics.DrawLabel(author.Name, authorIcon, default, authorRect, ContentAlignment.TopLeft, mousePosition: CursorLocation);
			}
			else
			{
				authorRect = e.Graphics.DrawLargeLabel(authorRect.Location, author.Name, authorImg, alignment: ContentAlignment.BottomLeft, padding: padding, height: height, cursorLocation: CursorLocation);
			}

			if (_compatibilityManager.IsUserVerified(author))
			{
				var avatarRect = authorRect.Pad(padding).Align(CompactList ? UI.Scale(new Size(18, 18), UI.FontScale) : new(authorRect.Height * 3 / 4, authorRect.Height * 3 / 4), ContentAlignment.MiddleLeft);
				var checkRect = avatarRect.Align(new Size(avatarRect.Height / 3, avatarRect.Height / 3), ContentAlignment.BottomRight);

				e.Graphics.FillEllipse(new SolidBrush(FormDesign.Design.GreenColor), checkRect.Pad(-(int)(2 * UI.FontScale)));

				using var img = IconManager.GetIcon("I_Check", checkRect.Height);

				e.Graphics.DrawImage(img.Color(Color.White), checkRect.Pad(0, 0, -1, -1));
			}

			e.Rects.AuthorRect = authorRect;

			return authorRect.Width;
		}

		private void DrawTitleAndTagsAndVersionForList(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, ILocalPackageData? localParentPackage, IWorkshopInfo? workshopInfo, bool isPressed)
		{
			using var font = UI.Font(GridView ? 10.5F : CompactList ? 8.25F : 9F, FontStyle.Bold);
			var mod = e.Item is not IAsset;
			var tags = new List<(Color Color, string Text)>();
			var text = mod ? e.Item.CleanName(out tags) : e.Item.ToString();
			using var brush = new SolidBrush(isPressed ? FormDesign.Design.ActiveForeColor : (e.Rects.CenterRect.Contains(CursorLocation) || e.Rects.IconRect.Contains(CursorLocation)) && e.HoverState.HasFlag(HoverState.Hovered) && !IsPackagePage ? FormDesign.Design.ActiveColor : ForeColor);
			e.Graphics.DrawString(text, font, brush, e.Rects.TextRect, new StringFormat { Trimming = StringTrimming.EllipsisCharacter, LineAlignment = CompactList ? StringAlignment.Center : StringAlignment.Near });

#if CS1
		var isVersion = localParentPackage?.Mod is not null && !e.Item.IsBuiltIn && !IsPackagePage;
		var versionText = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.IsBuiltIn ? Locale.Vanilla : e.Item is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
			var isVersion = !string.IsNullOrWhiteSpace(localParentPackage?.Version);
			var versionText = isVersion ? "v" + localParentPackage!.Version : e.Item is ILocalPackageData lp ? lp.FileSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#endif
			var date = workshopInfo?.ServerTime ?? e.Item.GetLocalPackage()?.LocalTime;

			var padding = GridView ? GridPadding : Padding;
			var textSize = e.Graphics.Measure(text, font);
			var tagRect = new Rectangle(e.Rects.TextRect.X + (int)textSize.Width, e.Rects.TextRect.Y, 0, e.Rects.TextRect.Height);

			for (var i = 0; i < tags.Count; i++)
			{
				var rect = e.Graphics.DrawLabel(tags[i].Text, null, tags[i].Color, tagRect, ContentAlignment.MiddleLeft, smaller: true);

				if (i == 0 && !string.IsNullOrEmpty(versionText))
				{
					e.Rects.VersionRect = rect;
				}

				tagRect.X += padding.Left + rect.Width;
			}

			if (CompactList)
			{
				var packageCol = _columnSizes[Columns.PackageName];
				using var backBrush = new SolidBrush(e.BackColor);
				e.Graphics.FillRectangle(backBrush, e.ClipRectangle.Pad(packageCol.X + packageCol.Width, 0, 0, 0));

				if (tagRect.X > packageCol.X + packageCol.Width)
				{
					DrawSeam(e, packageCol.X + packageCol.Width);
				}

				e.Rects.TextRect.Width = packageCol.Width - e.Rects.TextRect.X;
				e.Rects.CenterRect = e.Rects.TextRect;

				if (!string.IsNullOrEmpty(versionText))
				{
					e.Rects.VersionRect = DrawCell(e, Columns.Version, versionText!, null, isVersion ? FormDesign.Design.YellowColor : FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.AccentBackColor, 40), active: isVersion);
				}

				if (date.HasValue && !IsPackagePage)
				{
					var dateText = _settings.UserSettings.ShowDatesRelatively ? date.Value.ToRelatedString(true, false) : date.Value.ToString("g");

					e.Rects.DateRect = DrawCell(e, Columns.UpdateTime, dateText, null);
				}

				return;
			}

			tagRect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Bottom + padding.Bottom, 0, 0);

			if (!string.IsNullOrEmpty(versionText))
			{
				e.Rects.VersionRect = e.Graphics.DrawLabel(versionText, null, isVersion ? FormDesign.Design.YellowColor : FormDesign.Design.YellowColor.MergeColor(FormDesign.Design.AccentBackColor, 40), tagRect, ContentAlignment.TopLeft, smaller: true, mousePosition: isVersion ? CursorLocation : null);

				tagRect.X += padding.Left + e.Rects.VersionRect.Width;
			}

			if (date.HasValue && !IsPackagePage)
			{
				var dateText = _settings.UserSettings.ShowDatesRelatively ? date.Value.ToRelatedString(true, false) : date.Value.ToString("g");

				e.Rects.DateRect = e.Graphics.DrawLabel(dateText, IconManager.GetSmallIcon("I_UpdateTime"), FormDesign.Design.AccentColor, tagRect, ContentAlignment.TopLeft, smaller: true, mousePosition: CursorLocation);
			}
		}

		private void DrawVersionAndDate(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, IPackage? package, ILocalPackageIdentity? localIdentity, IWorkshopInfo? workshopInfo)
		{
#if CS1
			var isVersion = localParentPackage?.Mod is not null && !e.Item.IsBuiltIn && !IsPackagePage;
			var versionText = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.IsBuiltIn ? Locale.Vanilla : e.Item is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
			var isVersion = package?.IsCodeMod ?? (false && !string.IsNullOrEmpty(package!.Version));
			var versionText = isVersion ? "v" + package!.Version : localIdentity?.FileSize.SizeString(0) ?? workshopInfo?.ServerSize.SizeString(0);
#endif
			var tagRect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Bottom + GridPadding.Bottom / 2, 0, 0);

			var rect = e.Graphics.DrawLabel(versionText, null, isVersion ? Color.FromArgb(125, FormDesign.Design.YellowColor) : default, tagRect, ContentAlignment.TopLeft, smaller: false);
			tagRect.X += GridPadding.Left + rect.Width;

			var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localIdentity?.LocalTime ?? default) : workshopInfo.ServerTime;

			if (date != default)
			{
				var dateText = _settings.UserSettings.ShowDatesRelatively ? date.ToRelatedString(true, false) : date.ToString("g");
				var isRecent = date > DateTime.UtcNow.AddDays(-7);

				e.Graphics.DrawLabel(dateText, IconManager.GetSmallIcon("I_UpdateTime"), isRecent ? Color.FromArgb(125, FormDesign.Design.ActiveColor) : default, tagRect, ContentAlignment.TopLeft, smaller: false);
			}
		}

		private void DrawVoteAndSubscribers(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, IWorkshopInfo? workshopInfo)
		{
			if (workshopInfo is null)
				return;

			var rect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Bottom +(int) (20*UI.FontScale) + GridPadding.Bottom / 2, 0, 0);

			e.Rects.ScoreRect = e.Graphics.DrawLabel(Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0"))
					, IconManager.GetSmallIcon(workshopInfo.HasVoted ? "I_VoteFilled" : "I_Vote")
					, workshopInfo!.HasVoted ? FormDesign.Design.GreenColor : default
					, rect
					, ContentAlignment.TopLeft
					, mousePosition: CursorLocation);

			rect.X += GridPadding.Left + e.Rects.ScoreRect.Width;

			e.Graphics.DrawLabel(Locale.SubscribersCount.FormatPlural(workshopInfo.Subscribers, workshopInfo.Subscribers.ToString("N0"))
					, IconManager.GetSmallIcon("I_People")
					, default
					, rect
					, ContentAlignment.TopLeft);
		}

		private Rectangles GenerateGridRectangles(IPackageIdentity item, Rectangle rectangle)
		{
			var rects = new Rectangles(item)
			{
				IconRect = rectangle.Pad(GridPadding).Align(UI.Scale(new Size(80, 80), UI.FontScale), ContentAlignment.TopLeft)
			};

			using var font = UI.Font(11.25F, FontStyle.Bold);
			rects.TextRect = rectangle.Pad(rects.IconRect.Width + GridPadding.Left * 2, GridPadding.Top, GridPadding.Right, rectangle.Height).AlignToFontSize(font, ContentAlignment.TopLeft);

			rects.IncludedRect = rects.TextRect.Align(UI.Scale(new Size(28, 28), UI.FontScale), ContentAlignment.TopRight);

			if (_settings.UserSettings.AdvancedIncludeEnable && item.GetPackage()?.IsCodeMod == true)
			{
				rects.EnabledRect = rects.IncludedRect;

				rects.IncludedRect.X -= rects.IncludedRect.Width;
			}

			rects.TextRect.Width = rects.IncludedRect.X - rects.TextRect.X;

			rects.CenterRect = rects.TextRect.Pad(-GridPadding.Horizontal, 0, 0, 0);

			return rects;
		}

		protected override void OnViewChanged()
		{
			base.OnViewChanged();

			if (GridView)
			{
				GridPadding = UI.Scale(new Padding(4), UI.UIScale);
			}
		}
	}
}