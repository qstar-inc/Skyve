﻿using System.Collections.Generic;

namespace Skyve.Domain.Systems;
public interface IInterfaceService
{
	void OpenParadoxLogin();
	void OpenOptionsPage();
	void ViewSpecificPackages(List<IPackageIdentity> packages, string title);
	void OpenPackagePage(IPackageIdentity package, bool openCompatibilityPage = false);
	void OpenPlaysetPage(IPlayset playset, bool settingsTab = false);
	bool AskForDependencyConfirmation(List<IPackageIdentity> packages, List<IPackageIdentity> dependencies);
	void OpenLogReport(bool save);
}
