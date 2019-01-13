using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asocjacje
{
    class AprioriAlgorithm
    {
        private List<List<string>> data = File.ReadAllLines("data.txt")
            .Select(x => x.Replace("[", String.Empty).Replace("]", string.Empty).Split(',').Select(y => y.Trim(' '))
                .ToList()).ToList();



        public AprioriAlgorithm()
        {

        }

        public IEnumerable<string> Execute(double minimumSupport)
        {


            IEnumerable<string> tokens = this.data.SelectMany(x => x).Distinct();
            IEnumerable<double> supports = tokens.Select(x => Support(
                 x));
            IEnumerable<string> frequent = tokens.Where(x => Support(x) > minimumSupport);
            IEnumerable<IEnumerable<string>> setPermutations = null;
            IEnumerable<string> lastBest = null;
            for (int k = 2; true; k++)
            {
                if (k == 2)
                {
                    setPermutations = frequent.Permutations(2).Where(x => Support(x) > minimumSupport).ToList();
                }
                else
                {



                    var query = from kelement in (from permutation in setPermutations
                                                  join permutation1 in setPermutations on string.Join("", permutation.Take(k - 2)) equals String
                                                      .Join(
                                                          "", permutation1.Take(k - 2))

                                                  select permutation.Concat(permutation1).Distinct())

                                select kelement;

                    setPermutations = query.Where(x => x.Count() == k).Where(x=> Support(x) > minimumSupport).ToList();
                    if (setPermutations.Count() == 0)
                    {
                        return lastBest;
                    }

                   lastBest = setPermutations.OrderByDescending(x => Support(x)).First();
                }
            }

        }

        private bool TrySelfJoin()
        {
            return true;
        }
        private double Support(string s) => Convert.ToDouble(this.data.Count(x => x.Contains(s))) / this.data.Count();

        private double Support(IEnumerable<string> s) => Convert.ToDouble(this.data.Count(x => s.All(y => x.Contains(y)))) / this.data.Count();

        private double Confidence(IEnumerable<string> x, IEnumerable<string> y) => Support(x.Union(y)) / Support(x);

        private double Lift(IEnumerable<string> x, IEnumerable<string> y) => Support(x.Union(y)) / (Support(x) * Support(y));

        private double Conviction(IEnumerable<string> x, IEnumerable<string> y) =>
            (1 - Support(y)) / (1 - Confidence(x, y));

    }
}
