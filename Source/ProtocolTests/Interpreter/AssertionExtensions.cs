using FluentAssertions;
using FunicularSwitch;

namespace ProtocolTests.Interpreter
{
	public static class AssertionExtensions
	{
		public static TAssert AssertOptionType<TBase, TAssert>(this Option<TBase> option)
		{
			option.IsSome().Should().BeTrue("Expected Some but got None");
			return option.Match(
				some => some.Should().BeOfType<TAssert>().Which,
				() => throw new System.Exception("Expected Some but got None"));
		}
	}
}