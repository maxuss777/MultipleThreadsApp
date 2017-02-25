using System.Data.Entity;
using MultipleThreadsApp.Models;

namespace MultipleThreadsApp.Context
{
	public class TestContext : DbContext
	{
		public TestContext() : base("TestContext")
		{}

		public DbSet<TestModel> TestModels { get; set; }
	}
}
