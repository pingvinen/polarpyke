using System;

namespace PolarPyke.Parsing.Ebnf
{
	public enum TokenType
	{
		Unknown = 0,

		/// <summary>
		/// one = 1;
		/// ^^^
		/// </summary>
		Identifier = 1,

		/// <summary>
		/// one = 1;
		///     ^
		/// </summary>
		Definition = 2,

		/// <summary>
		/// one = "string";
		///        ^^^^^^
		/// </summary>
		String = 3,

		/// <summary>
		/// one = 1 | "one";
		///         ^
		/// </summary>
		Alternation = 4,

		/// <summary>
		/// name = first, [last];
		///               ^^^^^^
		/// </summary>
		Optional = 5,

		/// <summary>
		/// name = first, [last];
		///             ^
		/// </summary>
		Concatenation = 6,

		/// <summary>
		/// name = first, [last];
		///                     ^
		/// </summary>
		Termination = 7
	}
}