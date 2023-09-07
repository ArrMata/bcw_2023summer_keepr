using Microsoft.AspNetCore.Http;

namespace bcw_2023summer_keepr
{
	public class GlobalRoutePrefixMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _prefix;

		public GlobalRoutePrefixMiddleware(RequestDelegate next, string prefix)
		{
			_next = next;
			_prefix = prefix;
		}

		public async Task InvokeAsync(HttpContext ctx)
		{
			ctx.Request.PathBase = new PathString(_prefix);
			await _next(ctx);
		}

	}
}