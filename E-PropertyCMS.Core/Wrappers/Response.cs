using System;
namespace E_PropertyCMS.Core.Wrappers
{
	public class Response<T>
	{
		public T Data { get; set; }
		public bool Succeeded { get; set; }
		public string[] Error { get; set; }
		public string Message { get; set; }

		public Response()
		{

		}

		public Response(T data)
		{
			Succeeded = true;
			Message = string.Empty;
			Error = null;
			Data = data;
		}
	}
}

