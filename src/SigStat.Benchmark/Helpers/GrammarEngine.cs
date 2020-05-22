using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SigStat.Benchmark
{
    /// <summary>
    /// A simple engine for generating all possible sentences of a formal language and also parsing them based on 
    /// predefined production rules.
    /// </summary>
    public static class GrammarEngine
    {
        public struct DerivedSentence
        {
            public string Sentence;
            public string DerivationPath;

            public DerivedSentence(string sentence, string derivationPath)
            {
                Sentence = sentence;
                DerivationPath = derivationPath;
            }
        }

        /// <summary>
        /// Represents
        /// </summary>
        public class ProductionRule
        {
            public bool IsStartSymbol = false;
            public string Pattern { get; set; }
            public string[] Fragments { get; set; }
            public ProductionRule(string pattern, params string[] fragments)
            {
                Pattern = pattern;
                Fragments = fragments;
            }

            public override string ToString()
            {
                return $"{Pattern} -> {string.Join(", ", Fragments)}";
            }
        }

        /// <summary>
        /// Parses the production rules from the given string. The first nonterminal symbol in the left 
        /// hand side of the first rule is treated as the start symbol.
        /// Nonterminal symbols must be enclosed into square brackets, eg [Symbol].
        /// All other characters are treated as terminal symbols.
        ///  [Verifier] -> [Feature]_[Classifier]
        ///  [Feature] -> X,Y,P,XY,YXP,XP,YP
        ///  [Classifier] -> DTW, HMM
        /// </summary>
        /// <param name="rulesString">The rules string.</param>
        /// <returns>An array of parsed production rules</returns>
        /// <exception cref="ArgumentException"></exception>
        public static ProductionRule[] ParseRules(string rulesString)
        {
            List<ProductionRule> rules = new List<ProductionRule>();
            var lines = rulesString.Split("\n").Select(l => l.Trim()).Where(l => l.Length > 0);
            foreach (var line in lines)
            {
                try
                {
                    if (line.Trim().StartsWith("//")) continue;
                    var rule = ParseRule(line);
                    if (rules.Count == 0)
                        rule.IsStartSymbol = true;
                    rules.Add(rule);
                }
                catch (Exception exc)
                {
                    throw new ArgumentException($"Error parsing line: {line}", exc);
                }
            }
            // longest rule should be evaluated first
            return rules.OrderByDescending(r => r.Pattern.Length).ToArray();
        }

        /// <summary>
        /// Generates all sentences for a given rule-set.
        /// </summary>
        /// <param name="rules">An array of production rules.</param>
        /// <param name="fragment">The current sentence fragment.</param>
        /// <param name="derivationPath">The current derivation path.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Expression contains nonterminal symbols without any matching production rule: {fragment}</exception>
        public static IEnumerable<DerivedSentence> GenerateAllSentences(ProductionRule[] rules, string fragment = null, string derivationPath = "")
        {
            if (fragment == null)
                fragment = rules.First(r => r.IsStartSymbol).Pattern;
            bool match = false;
            for (int r = 0; r < rules.Length; r++)
            {
                var rule = rules[r];
                int firstIndex = fragment.IndexOf(rule.Pattern, StringComparison.Ordinal);
                if (firstIndex > -1)
                {
                    match = true;
                    for (int t = 0; t < rule.Fragments.Length; t++)
                    {
                        var term = rule.Fragments[t];
                        string prefix = fragment.Substring(0, firstIndex);
                        string postfix = fragment.Substring(firstIndex + rule.Pattern.Length);
                        string newFragment = prefix + term + postfix;
                        string newDerivationPath = derivationPath + (char)(r + 97) + t;
                        if (!newFragment.Contains("["))
                        {
                            yield return new DerivedSentence(newFragment, newDerivationPath);
                        }
                        else
                        {
                            foreach (var result in GenerateAllSentences(rules, newFragment, newDerivationPath))
                            {
                                yield return result;
                            }
                        }
                    }
                    yield break;
                }
            }
            if (!match)
            {
                throw new InvalidOperationException($"Expression contains nonterminal symbols without any matching production rule: {fragment}");
            }

        }

        /// <summary>
        /// Determines the sequence of production rules used to generate the sentence
        /// </summary>
        /// <param name="sentence">The sentence.</param>
        /// <param name="rules">The rules of the grammar.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Sentence '{sentence}'can not be generated by the rules</exception>
        public static Dictionary<string, string> ParseSentence(string sentence, ProductionRule[] rules, string whiteSpaceCharacters = "\t _")
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string derivationPath = null;
            foreach (var derivedSentence in GenerateAllSentences(rules))
            {
                if (derivedSentence.Sentence == sentence)
                {
                    derivationPath = derivedSentence.DerivationPath;
                    break;
                }
            }
            if (derivationPath == null)
                throw new KeyNotFoundException($"Sentence '{sentence}'can not be generated by the rules");

            Regex rx = new Regex("([a-z]+)([0-9]+)");
            Regex nonTerminalRx = new Regex("\\[[a-zA-Z0-9]*\\]");

            foreach (Match match in rx.Matches(derivationPath))
            {
                int ruleIndex = (byte)match.Groups[1].Value[0] - 97;
                int fragmentIndex = int.Parse(match.Groups[2].Value);
                var rule = rules[ruleIndex];
                var fragment = rule.Fragments[fragmentIndex];
                var fragmentWithoutNonterminals = nonTerminalRx.Replace(fragment, "");
                foreach (var ch in whiteSpaceCharacters)
                {
                    fragmentWithoutNonterminals = fragmentWithoutNonterminals.Replace(ch.ToString(), "");
                }
                result.Add(rule.Pattern.Replace("[", "").Replace("]", ""), fragmentWithoutNonterminals);

            };
            return result;
        }

        /// <summary>
        /// Expects a production rule in the following format:
        /// [Non terminal symbol] -> any combination of [terminal] and nonterminal symbols
        /// </summary>
        /// <param name="ruleString">The rule represented as a string</param>
        /// <returns></returns>
        public static ProductionRule ParseRule(string ruleString)
        {
            string[] mainParts = ruleString.Split("->");
            string pattern = mainParts[0].Trim();
            //if (!pattern.StartsWith("["))
            //    pattern = $"[{pattern}]";
            string[] terms = mainParts[1].Split("|").Select(s => s.Trim()).ToArray();
            if (terms.Any(t => t.StartsWith("*")))
                terms = terms.Where(t => t.StartsWith("*")).Select(t => t.Substring(1)).ToArray();
            return new ProductionRule(pattern, terms);
        }
    }
}
