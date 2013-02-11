using System;
using NUnit.Framework;
using System.Collections.Generic;
using PolarPyke.Parsing.Ebnf;

namespace libtests.Parsing.Ebnf
{
	[TestFixture]
	public class TokenizerTests
	{
		[Test]
		public void OneRule_Valid()
		{
			string ebnf = "space = \" \";";

			List<Token> expected = new List<Token>();
			expected.Add(new Token() {
				CharacterPosition = 0,
				LineNumber = 0,
				Type = TokenType.Identifier,
				Content = "space"
			});
			expected.Add(new Token() {
				CharacterPosition = 6,
				LineNumber = 0,
				Type = TokenType.Definition,
				Content = String.Empty
			});
			expected.Add(new Token() {
				CharacterPosition = 8,
				LineNumber = 0,
				Type = TokenType.String,
				Content = " "
			});
			expected.Add(new Token() {
				CharacterPosition = 11,
				LineNumber = 0,
				Type = TokenType.Termination
			});

			EbnfTokenizer nizer = new EbnfTokenizer();
			IList<Token> actual = nizer.Tokenize(ebnf);

			CollectionAssert.AreEqual(expected, actual);
		}
	}
}