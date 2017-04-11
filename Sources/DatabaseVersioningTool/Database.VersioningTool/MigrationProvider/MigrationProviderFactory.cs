using System;
using Database.VersioningTool.Configuration;

namespace Database.VersioningTool.MigrationProvider
{
	public class MigrationProviderFactory
	{
		private MigrationProviderType _providerType;
		private IConfigurationManager _configurationManager;

		public MigrationProviderFactory(IConfigurationManager configurationManager, MigrationProviderType providerType)
		{
			if (configurationManager == null)
			{
				throw new ArgumentNullException("Configuration manager is not set", "configurationManager");
			}

			_configurationManager = configurationManager;
			_providerType = providerType;
		}

		public IMigrationProvider GetMigrationProvider()
		{
			switch (_providerType)
			{
				case MigrationProviderType.File:
					return new FileMigrationProvider(_configurationManager);
				default:
				case MigrationProviderType.None:
					throw new ArgumentException("Provider type not set");
			}
		}
	}
}
