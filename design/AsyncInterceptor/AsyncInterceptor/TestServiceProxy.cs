﻿using System.Threading.Tasks;

namespace AsyncInterceptor
{
    public class TestServiceProxy : ITestService
    {
        private readonly ITestService realService;
        private readonly IInterceptor interceptor;

        public TestServiceProxy(ITestService realService, IInterceptor interceptor)
        {
            this.realService = realService;
            this.interceptor = interceptor;
        }

        private void SumReal(Context context)
        {
            var x = (int)context.Parameters[0];
            var y = (int)context.Parameters[1];
            context.Result = realService.Sum(x, y);
        }

        public int Sum(int x, int y)
        {
            var context = new Context()
            {
                Parameters = new object[] { x, y }
            };
            interceptor.OnInvokeSync(context, SumReal);
            return (int)context.Result;
        }

        private async Task SumAsyncReal(Context context)
        {
            var x = (int)context.Parameters[0];
            var y = (int)context.Parameters[1];
            context.Result = await realService.SumAsync(x, y);
        }

        public async Task<int> SumAsync(int x, int y)
        {
            var context = new Context()
            {
                Parameters = new object[] { x, y }
            };
            await interceptor.OnInvokeAsync(context, SumAsyncReal);
            return (int)context.Result;
        }

        private void NoAwaitSumAsyncReal(Context context)
        {
            var x = (int)context.Parameters[0];
            var y = (int)context.Parameters[1];
            context.Result = realService.SumAsync(x, y);
        }

        public Task<int> NoAwaitSumAsync(int x, int y)
        {
            var context = new Context()
            {
                Parameters = new object[] { x, y }
            };
            interceptor.OnInvokeSync(context, NoAwaitSumAsyncReal);
            return (Task<int>)context.Result;
        }
    }
}