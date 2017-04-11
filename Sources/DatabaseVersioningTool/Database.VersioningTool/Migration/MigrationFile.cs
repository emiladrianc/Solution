using System;

namespace Database.VersioningTool.Migration
{
	public class MigrationFile : IMigrationFile
	{
		public int Order { get; private set; }

		public string Name { get; private set; }

		public string Content { get; private set; }

		public MigrationFile(int order, string name, string content)
		{
			Order = order;
			Name = name;
			Content = content;

		}

		public bool IsValid()
		{
			if (Order <= 0)
				return false;

			if (string.IsNullOrEmpty(Name))
				return false;

			if (string.IsNullOrEmpty(Content))
				return false;

			return true;
		}
	}
}
