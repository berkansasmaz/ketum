using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ketum.Web.Models
{
    public class KTReturn
    {
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string InternalMessage { get; set; }
		public int Code { get; set; } //Bu code http status değil.  Status kod da olabilir ama biz ilgili hata kodu olarak da kullanabiliriz atıyorum 1001 kodu benim için bu kullanıcı daha önce kaydedilmiştir diyip bu hata kodu kullanıcı verirsem ve bunun yanında hata kodların anlamları api mi kullananlara iletirsem daha anlaşılabilir olabilir. 
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public object Data { get; set; }
		public bool Success { get; set; }
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<KTReturnError> Errors { get; set; } // Biz kullanıcıya mesaj döneceğimiz zaman birden fazla hata mesajı dönüyorsak bu kısma ekliyoruz.
    }

	   public class KTReturnError
    {
        public string Message { get; set; }
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string InternalMessage { get; set; }
		public string Name { get; set; }
    }
}