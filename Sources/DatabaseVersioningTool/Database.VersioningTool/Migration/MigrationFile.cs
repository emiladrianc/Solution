using System;

namespace Database.VersioningTool.Migration
{
	public class MigrationFile
    {
		public int Order;

		public string Name;

		public string Content;

		public MigrationFile(int order, string name, string content)
		{
			if (order <= 0)
			{
				throw new ArgumentException("Order should be a positive non-zero number", "order");
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name should be a valid string not empty", "name");
			}

			if (string.IsNullOrEmpty(content))
			{
				throw new ArgumentException("Content should be a valid string not empty", "content");
			}

			Order = order;
			Name = name;
			Content = content;

		}
    }
}
