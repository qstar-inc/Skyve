﻿using System.Threading.Tasks;

namespace Skyve.App.UserInterface.Panels;
public class PC_Mods : PC_ContentList
{
	public PC_Mods()
	{
		LC_Items.OT_Included.SelectedValue = Generic.ThreeOptionToggle.Value.Option1;
	}

	public override SkyvePage Page => SkyvePage.Mods;

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		Text = $"{Locale.Mod.Plural} - {ServiceCenter.Get<IPlaysetManager>().CurrentPlayset?.Name ?? Locale.NoActivePlayset}";
	}

	protected override async Task<IEnumerable<IPackageIdentity>> GetItems()
	{
		return await Task.FromResult(ServiceCenter.Get<IPackageManager>().Packages);
	}

	protected override string GetCountText()
	{
		var mods = LC_Items.Items;
		var modsIncluded = mods.Count(x => x.IsIncluded());
		var modsEnabled = mods.Count(x => x.IsEnabled() && x.IsIncluded());
		var total = LC_Items.ItemCount;

		if (!ServiceCenter.Get<ISettings>().UserSettings.AdvancedIncludeEnable)
		{
			return string.Format(Locale.ModIncludedTotal, modsIncluded, total);
		}

		if (modsIncluded == modsEnabled)
		{
			return string.Format(Locale.ModIncludedAndEnabledTotal, modsIncluded, total);
		}

		return string.Format(Locale.ModIncludedEnabledTotal, modsIncluded, modsEnabled, total);
	}

	protected override LocaleHelper.Translation GetItemText()
	{
		return Locale.Mod;
	}
}
