﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Skyve.App.UserInterface.Lists;

public partial class ItemListControl
{
	private int DrawScore(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, IWorkshopInfo? workshopInfo, int xdiff)
	{
		if (workshopInfo is null)
		{
			return 0;
		}

		var score = workshopInfo.VoteCount;
		var padding = GridView ? GridPadding : Padding;
		var height = e.Rects.IconRect.Bottom - Math.Max(e.Rects.TextRect.Bottom, Math.Max(e.Rects.VersionRect.Bottom, e.Rects.DateRect.Bottom)) - padding.Bottom;

		e.Rects.ScoreRect = e.Graphics.DrawLargeLabel(new Point(e.Rects.TextRect.X + xdiff + (xdiff == 0 ? 0 : padding.Left), e.Rects.IconRect.Bottom), score.ToMagnitudeString(), "I_VoteFilled", workshopInfo!.HasVoted ? FormDesign.Design.GreenColor : null, alignment: ContentAlignment.BottomLeft, padding: padding, height: height, cursorLocation: CursorLocation);

		return 0;
	}

	private void DrawThumbnail(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, ILocalPackageIdentity? localIdentity, IWorkshopInfo? workshopInfo)
	{
		var thumbnail = e.Item.GetThumbnail();

		if (thumbnail is null)
		{
			using var generic = IconManager.GetIcon(e.Item is IAsset ? "I_Assets" : _page is SkyvePage.Mods ? "I_Mods" : _page is SkyvePage.Packages ? "I_Package" : "I_Paradox", e.Rects.IconRect.Height).Color(e.BackColor);
			using var brush = new SolidBrush(FormDesign.Design.IconColor);

			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, (int)(5 * UI.FontScale));
			e.Graphics.DrawImage(generic, e.Rects.IconRect.CenterR(generic.Size));
		}
		else if (e.Item.IsLocal())
		{
			using var unsatImg = new Bitmap(thumbnail, e.Rects.IconRect.Size).Tint(Sat: 0);

			drawThumbnail(unsatImg);
		}
		else
		{
			drawThumbnail(thumbnail);
		}

		if (e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.IconRect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(75, 255, 255, 255));
			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, (int)(5 * UI.FontScale));
		}

		var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localIdentity?.LocalTime ?? default) : workshopInfo.ServerTime;
		var isRecent = date > DateTime.UtcNow.AddDays(-7);

		if (isRecent && !IsPackagePage)
		{
			using var pen = new Pen(FormDesign.Design.ActiveColor, (float)(2 * UI.FontScale));

			if (!GridView || _settings.UserSettings.ExtendedListInfo)
			{
				e.Graphics.DrawRoundedRectangle(pen, e.Rects.IconRect, (int)(5 * UI.FontScale));

				return;
			}

			using var font = UI.Font(9F, FontStyle.Bold);
			using var gradientBrush = new LinearGradientBrush(e.Rects.IconRect.Pad(0, e.Rects.IconRect.Height * 2 / 3, 0, 0), Color.Empty, FormDesign.Design.ActiveColor, 90);
			using var dateBrush = new SolidBrush(FormDesign.Design.ActiveForeColor);
			using var stringFormat = new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center };

			var cblend = new ColorBlend(4)
			{
				Colors = [default, Color.FromArgb(100, FormDesign.Design.ActiveColor), Color.FromArgb(200, FormDesign.Design.ActiveColor), FormDesign.Design.ActiveColor],
				Positions = [0f, 0.5f, 0.7f, 1f]
			};

			gradientBrush.InterpolationColors = cblend;

			e.Graphics.FillRoundedRectangle(gradientBrush, e.Rects.IconRect.Pad(0, e.Rects.IconRect.Height * 2 / 3, 0, 0), (int)(5 * UI.FontScale));
			e.Graphics.DrawRoundedRectangle(pen, e.Rects.IconRect, (int)(5 * UI.FontScale));

			e.Graphics.DrawString(Locale.RecentlyUpdated, font, dateBrush, e.Rects.IconRect.Pad(GridPadding), stringFormat);
		}

		void drawThumbnail(Bitmap generic) => e.Graphics.DrawRoundedImage(generic, e.Rects.IconRect, (int)(5 * UI.FontScale), FormDesign.Design.BackColor/*, blur: e.Rects.IconRect.Contains(CursorLocation)*/);
	}

#if CS2
	private void DrawIncludedButton(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, bool isIncluded, bool isPartialIncluded, bool isEnabled, ILocalPackageIdentity? localIdentity, out Color activeColor)
	{
		activeColor = default;

		if (localIdentity is null && e.Item.IsLocal())
		{
			return; // missing local item
		}

		var required = _modLogicManager.IsRequired(localIdentity, _modUtil);
		var isHovered = e.DrawableItem.Loading || (e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.IncludedRect.Contains(CursorLocation));

		if (!required && isIncluded && isHovered)
		{
			isPartialIncluded = false;
			isEnabled = !isEnabled;
		}

		if (isEnabled)
		{
			activeColor = isPartialIncluded ? FormDesign.Design.YellowColor : FormDesign.Design.GreenColor;
		}

		Color iconColor;

		if (required && activeColor != default)
		{
			iconColor = FormDesign.Design.Type is FormDesignType.Light ? activeColor.MergeColor(ForeColor, 75) : activeColor;
			activeColor = activeColor.MergeColor(BackColor, FormDesign.Design.Type is FormDesignType.Light ? 35 : 20);
		}
		else if (activeColor == default && isHovered)
		{
			iconColor = isIncluded ? isEnabled ? FormDesign.Design.GreenColor : FormDesign.Design.RedColor : FormDesign.Design.ActiveColor;
			activeColor = Color.FromArgb(40, iconColor);
		}
		else
		{
			if (activeColor == default)
			{
				activeColor = Color.FromArgb(20, ForeColor);
			}
			else if (isHovered)
			{
				activeColor = activeColor.MergeColor(ForeColor, 75);
			}

			iconColor = activeColor.GetTextColor();
		}

		using var brush = e.Rects.IncludedRect.Gradient(activeColor);
		e.Graphics.FillRoundedRectangle(brush, e.Rects.IncludedRect, (int)(4 * UI.FontScale));

		if (e.DrawableItem.Loading)
		{
			DrawLoader(e.Graphics, e.Rects.IncludedRect.CenterR(e.Rects.IncludedRect.Height / 2, e.Rects.IncludedRect.Height / 2), iconColor);
			return;
		}

		var icon = new DynamicIcon(_subscriptionsManager.IsSubscribing(e.Item) ? "I_Wait" : isPartialIncluded ? "I_Slash" : isEnabled ? "I_Ok" : !isIncluded ? "I_Add" : "I_Enabled");
		using var includedIcon = icon.Get(e.Rects.IncludedRect.Height * 3 / 4).Color(iconColor);

		e.Graphics.DrawImage(includedIcon, e.Rects.IncludedRect.CenterR(includedIcon.Size));
	}
#else
		private void DrawIncludedButton(ItemPaintEventArgs<T, Rectangles> e, bool isIncluded, bool partialIncluded, ILocalPackageData? package, out Color activeColor)
		{
			activeColor = default;

			if (package is null && e.Item.IsLocal())
			{
				return; // missing local item
			}

			var inclEnableRect = e.Rects.EnabledRect == Rectangle.Empty ? e.Rects.IncludedRect : Rectangle.Union(e.Rects.IncludedRect, e.Rects.EnabledRect);
			var incl = new DynamicIcon(_subscriptionsManager.IsSubscribing(e.Item) ? "I_Wait" : partialIncluded ? "I_Slash" : isIncluded ? "I_Ok" : package is null ? "I_Add" : "I_Enabled");
			var required = package is not null && _modLogicManager.IsRequired(package, _modUtil);

			DynamicIcon? enabl = null;

			if (_settings.UserSettings.AdvancedIncludeEnable && package is not null)
			{
				enabl = new DynamicIcon(package.IsEnabled() ? "I_Checked" : "I_Checked_OFF");

				if (isIncluded)
				{
					activeColor = partialIncluded ? FormDesign.Design.YellowColor : package.IsEnabled() ? FormDesign.Design.GreenColor : FormDesign.Design.RedColor;
				}
				else if (package.IsEnabled())
				{
					activeColor = FormDesign.Design.YellowColor;
				}
			}
			else if (isIncluded)
			{
				activeColor = partialIncluded ? FormDesign.Design.YellowColor : FormDesign.Design.GreenColor;
			}

			Color iconColor;

			if (required && activeColor != default)
			{
				iconColor = FormDesign.Design.Type is FormDesignType.Light ? activeColor.MergeColor(ForeColor, 75) : activeColor;
				activeColor = activeColor.MergeColor(BackColor, FormDesign.Design.Type is FormDesignType.Light ? 35 : 20);
			}
			else if (activeColor == default && inclEnableRect.Contains(CursorLocation))
{
				activeColor = Color.FromArgb(40, FormDesign.Design.ActiveColor);
				iconColor = FormDesign.Design.ActiveColor;
			}
			else
			{
				if (activeColor == default)
					activeColor = Color.FromArgb(20, ForeColor);
				else if (inclEnableRect.Contains(CursorLocation))
					activeColor = activeColor.MergeColor(ForeColor, 75);

				iconColor = activeColor.GetTextColor();
			}

			using var brush = inclEnableRect.Gradient(activeColor);

			e.Graphics.FillRoundedRectangle(brush, inclEnableRect, (int)(4 * UI.FontScale));

			using var includedIcon = incl.Get(e.Rects.IncludedRect.Width * 3 / 4).Color(iconColor);
			using var enabledIcon = enabl?.Get(e.Rects.IncludedRect.Width * 3 / 4).Color(iconColor);

			e.Graphics.DrawImage(includedIcon, e.Rects.IncludedRect.CenterR(includedIcon.Size));
			if (enabledIcon is not null)
			{
				e.Graphics.DrawImage(enabledIcon, e.Rects.EnabledRect.CenterR(includedIcon.Size));
			}
		}
#endif
	private void DrawTitleAndTags(ItemPaintEventArgs<IPackageIdentity, Rectangles> e)
	{
		using var font = UI.Font(GridView ? (_settings.UserSettings.ExtendedListInfo ? 11.25F : 9F) : CompactList ? 8.25F : 10.5F, FontStyle.Bold);
		using var brushTitle = new SolidBrush(e.Rects.CenterRect.Contains(CursorLocation) && e.HoverState == HoverState.Hovered && !IsPackagePage ? FormDesign.Design.ActiveColor : e.BackColor.GetTextColor());
		using var stringFormat = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, LineAlignment = CompactList ? StringAlignment.Center : StringAlignment.Near };
		var text = e.Item.CleanName(out var tags);

		e.Graphics.DrawString(text, font, brushTitle, e.Rects.TextRect, stringFormat);

		var padding = GridView ? GridPadding : Padding;
		var textSize = e.Graphics.Measure(text, font);
		var tagRect = new Rectangle(e.Rects.TextRect.X + (int)textSize.Width, e.Rects.TextRect.Y, 0, e.Rects.TextRect.Height);

		for (var i = 0; i < tags.Count; i++)
		{
			var rect = e.Graphics.DrawLabel(tags[i].Text, null, tags[i].Color, tagRect, ContentAlignment.MiddleLeft, smaller: !_settings.UserSettings.ExtendedListInfo);

			tagRect.X += padding.Left + rect.Width;
		}
	}

	private void DrawCompactVersionAndDate(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, IPackage? package, ILocalPackageIdentity? localIdentity, IWorkshopInfo? workshopInfo)
	{
#if CS1
		var isVersion = localParentPackage?.Mod is not null && !e.Item.IsBuiltIn && !IsPackagePage;
		var text = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.IsBuiltIn ? Locale.Vanilla : e.Item is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
		var isVersion = package?.IsCodeMod ?? (false && !string.IsNullOrEmpty(package!.Version));
		var text = isVersion ? "v" + package!.Version : localIdentity?.FileSize.SizeString(0) ?? workshopInfo?.ServerSize.SizeString(0);
#endif
		var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localIdentity?.LocalTime ?? default) : workshopInfo.ServerTime;

		if (text is not null and not "")
		{
			DrawCell(e, Columns.Version, text, null, active: false);
		}

		if (Width / UI.FontScale >= 800 && date != default)
		{
			var dateText = _settings.UserSettings.ShowDatesRelatively ? date.ToRelatedString(true, false) : date.ToString("g");
			DrawCell(e, Columns.UpdateTime, dateText, "I_UpdateTime", active: false);
		}
	}

	private Rectangle DrawCell(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, Columns column, string text, DynamicIcon? dIcon, Color? backColor = null, Font? font = null, bool active = true, Padding padding = default)
	{
		var cell = _columnSizes[column];
		var rect = new Rectangle(cell.X, e.ClipRectangle.Y, cell.Width, e.ClipRectangle.Height).Pad(0, -Padding.Top, 0, -Padding.Bottom);

		var textColor = backColor?.GetTextColor() ?? e.BackColor.GetTextColor();

		if (active && rect.Contains(CursorLocation))
		{
			textColor = FormDesign.Design.ActiveColor;
		}

		if (dIcon != null)
		{
			using var icon = dIcon.Small.Color(textColor);

			e.Graphics.DrawImage(icon, rect.Pad(Padding).Align(icon.Size, ContentAlignment.MiddleLeft));

			rect = rect.Pad(icon.Width + Padding.Left, 0, 0, 0);
		}

		using (var brush = new SolidBrush(textColor))
		using (font ??= UI.Font(7.5F))
		{
			var textRect = rect.Pad(padding + Padding).AlignToFontSize(font, ContentAlignment.MiddleLeft, e.Graphics);

			e.Graphics.DrawString(text, font, brush, textRect, new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

			if (e.Graphics.Measure(text, font).Width <= rect.Right - textRect.X)
			{
				return rect;
			}
		}

		using var backBrush = new SolidBrush(e.BackColor);
		e.Graphics.FillRectangle(backBrush, e.ClipRectangle.Pad(rect.Right, 0, 0, 0));

		DrawSeam(e, rect.Right);

		return rect;
	}

	private static void DrawSeam(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, int x)
	{
		var seamRectangle = new Rectangle(x - (int)(40 * UI.UIScale), e.ClipRectangle.Y, (int)(40 * UI.UIScale), e.ClipRectangle.Height);

		using var seamBrush = new LinearGradientBrush(seamRectangle, Color.Empty, e.BackColor, 0F);

		e.Graphics.FillRectangle(seamBrush, seamRectangle);
	}

	protected override void DrawHeader(PaintEventArgs e)
	{
		var headers = new List<(string text, int width)?>
			{
				(Locale.Package, 0),
				(Locale.Version, 65),
				(Locale.UpdateTime, 140),
				(Locale.Author, 150),
				(Locale.Tags, 0),
				(Locale.Status, 160),
				("", 80)
			};

		if (Width / UI.FontScale < 500)
		{
			headers[5] = null;
		}

		if (Width / UI.FontScale < 965)
		{
			headers[4] = null;
		}

		if (Width / UI.FontScale < 600)
		{
			headers[3] = null;
		}

		if (Width / UI.FontScale < 800)
		{
			headers[2] = null;
		}

		var remainingWidth = Width - (int)(headers.Sum(x => x?.width ?? 0) * UI.FontScale);
		var autoColumns = headers.Count(x => x?.width == 0);
		var xPos = 0;

		using var font = UI.Font(7.5F, FontStyle.Bold);
		using var brush = new SolidBrush(FormDesign.Design.LabelColor);
		using var lineBrush = new SolidBrush(FormDesign.Design.AccentColor);

		e.Graphics.Clear(FormDesign.Design.AccentBackColor);

		e.Graphics.FillRectangle(lineBrush, new Rectangle(0, StartHeight - 2, Width, 2));

		for (var i = 0; i < headers.Count; i++)
		{
			var header = headers[i];

			if (header == null)
			{
				continue;
			}

			var width = header.Value.width == 0 ? (remainingWidth / autoColumns) : (int)(header.Value.width * UI.FontScale);

			e.Graphics.DrawString(header.Value.text.ToUpper(), font, brush, new Rectangle(xPos, 1, width, StartHeight).Pad(Padding).AlignToFontSize(font, ContentAlignment.MiddleLeft), new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });

			_columnSizes[(Columns)i] = (xPos, width);

			xPos += width;
		}
	}

	private void DrawCompactAuthorOrFolder(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, ILocalPackageIdentity? localIdentity, IWorkshopInfo? workshopInfo)
	{
		var author = workshopInfo?.Author;

		if (author?.Name is not null and not "")
		{
			e.Rects.AuthorRect = DrawCell(e, Columns.Author, author.Name, "I_Author", font: UI.Font(8.25F));
		}
		else if (localIdentity is not null)
		{
			DrawCell(e, Columns.Author, Path.GetFileName(localIdentity.Folder), "I_Folder", active: false, font: UI.Font(8.25F));
		}
	}

	private int DrawButtons(ItemPaintEventArgs<IPackageIdentity, Rectangles> e, ILocalPackageIdentity? parentPackage, IWorkshopInfo? workshopInfo)
	{
		var padding = GridView ? GridPadding : Padding;
		var size = _settings.UserSettings.ExtendedListInfo ?
			UI.Scale(CompactList ? new Size(24, 24) : new Size(28, 28), UI.FontScale) :
			UI.Scale(CompactList ? new Size(20, 20) : new Size(24, 24), UI.FontScale);
		var rect = new Rectangle(e.ClipRectangle.Right, e.ClipRectangle.Y, 0, e.ClipRectangle.Height).Pad(padding);
		var backColor = Color.FromArgb(175, GridView ? FormDesign.Design.BackColor : FormDesign.Design.ButtonColor);

		if (parentPackage is not null)
		{
			e.Rects.FolderRect = SlickButton.AlignAndDraw(e.Graphics, rect, CompactList ? ContentAlignment.MiddleRight : ContentAlignment.BottomRight, new ButtonDrawArgs
			{
				Size = size,
				Icon = "I_Folder",
				Font = Font,
				HoverState = e.HoverState,
				Cursor = CursorLocation
			}).Rectangle;

			rect.X -= e.Rects.FolderRect.Width + padding.Left;
		}

		if (!IsPackagePage && workshopInfo?.Url is not null)
		{
			e.Rects.SteamRect = SlickButton.AlignAndDraw(e.Graphics, rect, CompactList ? ContentAlignment.MiddleRight : ContentAlignment.BottomRight, new ButtonDrawArgs
			{
				Size = size,
#if CS2
				Icon = "I_Paradox",
#else
				Icon = "I_Steam",
#endif
				Font = Font,
				HoverState = e.HoverState,
				Cursor = CursorLocation
			}).Rectangle;

			rect.X -= e.Rects.SteamRect.Width + padding.Left;
		}

		if (!IsPackagePage && _settings.UserSettings.ExtendedListInfo && _compatibilityManager.GetPackageInfo(e.Item)?.Links?.FirstOrDefault(x => x.Type == LinkType.Github) is ILink gitLink)
		{
			e.Rects.GithubRect = SlickButton.AlignAndDraw(e.Graphics, rect, CompactList ? ContentAlignment.MiddleRight : ContentAlignment.BottomRight, new ButtonDrawArgs
			{
				Size = size,
				Icon = "I_Github",
				Font = Font,
				HoverState = e.HoverState,
				Cursor = CursorLocation
			}).Rectangle;

			rect.X -= e.Rects.GithubRect.Width + padding.Left;
		}

		return rect.X + rect.Width;
	}

}