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
		public void OrderShouldBePositive()
		{
			Assert.Throws<ArgumentException>(() => new MigrationFile(-1, null, null));
		}

		[Fact]
		public void OrderShouldNotBeZero()
		{
			Assert.Throws<ArgumentException>(() => new MigrationFile(0, null, null));
		}

		[Fact]
		public void TestGetOrder()
		{
			var migrationFile = new MigrationFile(_validOrder, _validName, _validContent);

			Assert.True(migrationFile.Order == _validOrder);
		}

		[Fact]
		public void NameShouldBeSet()
		{
			Assert.Throws<ArgumentException>(() => new MigrationFile(_validOrder, null, null));
		}

		[Fact]
		public void NameShouldNotBeEmpty()
		{
			Assert.Throws<ArgumentException>(() => new MigrationFile(_validOrder, string.Empty, null));
		}

		[Fact]
		public void TestGetName()
		{			
			var migrationFile = new MigrationFile(_validOrder, _validName, _validContent);

			Assert.True(migrationFile.Name == _validName);
		}

		[Fact]
		public void ContentShouldBeSet()
		{
			Assert.Throws<ArgumentException>(() => new MigrationFile(_validOrder, _validName, null));
		}

		[Fact]
		public void ContentShouldNotBeEmpty()
		{
			Assert.Throws<ArgumentException>(() => new MigrationFile(_validOrder, _validName, string.Empty));
		}

		[Fact]
		public void TestGetContent()
		{
			var migrationFile = new MigrationFile(_validOrder, _validName, _validContent);

			Assert.True(migrationFile.Content == _validContent);
		}

	}
}
