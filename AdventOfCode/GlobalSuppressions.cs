// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Don't want to do it", Scope = "module")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "Not regularly called", Scope = "member", Target = "~M:AdventOfCode._2021.Day04.BingoBoard.#ctor(System.String)")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "Not regularly called", Scope = "member", Target = "~M:AdventOfCode._2022.Day05.ParseInstruction(System.String)~AdventOfCode._2022.Day05.Instruction")]
