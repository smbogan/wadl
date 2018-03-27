using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wadl.Binary;
using System.Linq;

namespace DevTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Random rand = new Random();

            {
                for (int i = 0; i < 500000; i++)
                {
                    int n = rand.Next();

                    var data = LEB128Encoding.EncodeInt32(n).ToArray();
                    int result = LEB128Encoding.DecodeInt32(data);

                    if (result != n)
                        throw new Exception();
                }
            }

            {
                for (int i = 0; i < 500000; i++)
                {
                    uint n = (uint)rand.Next();

                    var data = LEB128Encoding.EncodeUInt32(n).ToArray();
                    uint result = LEB128Encoding.DecodeUInt32(data);

                    if (result != n)
                        throw new Exception();
                }
            }

        }
    }
}
