﻿using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace SyncInterceptor
{
    [AllStatisticsColumn]
    public class TestSyncInterceptor
    {
        private readonly int x = 40;
        private readonly int y = 4;
        private readonly ITestService noInterceptor = new TestService();
        private readonly ITestService handwritingInterceptor = new TestServiceHandWriting(new TestService());
        private readonly ITestService syncInterceptor = new TestServiceProxy(new TestService(), new TestInterceptor());
        private readonly ITestService syncNoParamInterceptor = new TestServiceProxy(new TestService(), new TestNoParamInterceptor());
        private readonly MethodInfo cacheMethod;

        public TestSyncInterceptor()
        {
            cacheMethod = typeof(TestSyncInterceptor).GetMethod("GetCurrentMethod");
        }

        [Benchmark]
        public void GetCurrentMethod()
        {
            var c = new Context()
            {
                ServiceMethod = MethodBase.GetCurrentMethod(),
                Parameters = new object[0]
            };
        }

        [Benchmark]
        public void GetCacheMethod()
        {
            var c = new Context()
            {
                ServiceMethod = cacheMethod,
                Parameters = new object[0]
            };
        }

        //[Benchmark]
        //public void SyncMethod_HasParam_NoInterceptor()
        //{
        //    noInterceptor.Sum(x, y);
        //}

        //[Benchmark]
        //public void SyncMethod_HasParam_HandWritingInterceptor()
        //{
        //    handwritingInterceptor.Sum(x, y);
        //}

        //[Benchmark]
        //public void SyncMethod_HasParam_ProxyAndInterceptor()
        //{
        //    syncInterceptor.Sum(x, y);
        //}

        //[Benchmark]
        //public void SyncMethod_NoParam_NoInterceptor()
        //{
        //    noInterceptor.Sum();
        //}

        //[Benchmark]
        //public void SyncMethod_NoParam_HandWritingInterceptor()
        //{
        //    handwritingInterceptor.Sum();
        //}

        //[Benchmark]
        //public void SyncMethod_NoParam_ProxyAndInterceptor()
        //{
        //    syncNoParamInterceptor.Sum();
        //}
    }
}