using System;
using Database.VersioningTool.Migration;
using Xunit;

namespace Database.VersioningTool.Tests.Migration
{

	public class MigrationFileTests
	{

		private const int _validOrder = 1;
		private const string _validName = "Name";
		private const string _validContent = "Content";

		[Fact]
		public void TestGetMethods()
		{
			var migrationFile = new MigrationFile(_validOrder, _validName, _validContent);

			Assert.True(migrationFile.Order == _validOrder);
			Assert.True(migrationFile.Name == _validName);
			Assert.True(migrationFile.Content == _validContent);
		}

		[Fact]
		public void OrderNotValid()
		{
			var migrationFile = new MigrationFile(-1, _validName, _validContent);

			Assert.False(migrationFile.IsValid());
		}

		[Fact]
		public void OrderShouldBePositive()
		{
			var migrationFile = new MigrationFile(0, _validName, _validContent);

			Assert.False(migrationFile.IsValid());
		}


		[Fact]
		public void NameShouldBeSet()
		{
			var migrationFile = new MigrationFile(1, null, _validContent);

			Assert.False(migrationFile.IsValid());
		}

		[Fact]
		public void NameShouldNotBeEmpty()
		{
			var migrationFile = new MigrationFile(1, string.Empty, _validContent);

			Assert.False(migrationFile.IsValid());
		}

		[Fact]
		public void ContentShouldBeSet()
		{
			var migrationFile = new MigrationFile(1, _validName, null);

			Assert.False(migrationFile.IsValid());
		}

		[Fact]
		public void ContentShouldNotBeEmpty()
		{
			var migrationFile = new MigrationFile(1, _validName, string.Empty);

			Assert.False(migrationFile.IsValid());
		}

		[Fact]
		public void IsValid()
		{
			var migrationFile = new MigrationFile(1, _validName, _validContent);

			Assert.True(migrationFile.IsValid());
		}

	}
}
