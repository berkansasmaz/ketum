import axios from "axios";
import router from "@/router";

export const http = axios.create({
  headers: {
    "Content-Type": "application/json",
    "X-Requested-With": "XMLHttpRequest",
    "X-Application-Name": "vue"
  }
});

http.interceptors.response.use(
  function (response) {
	return response
	//interceptors ilk parametre olarak response iletimeden önceki halini alır bu kısmı olduğu gibi bırakıtoruz
  },
  function (error) {
	  //İkinci parametre olarak response' un iletildikten sonra ki halini alır. 
    const statusCode = error.response.status;
    if (statusCode === 401) {
      window.location.href = "/Identity/Account/Login?ReturnUrl=" + encodeURIComponent(window.location.pathname); // Login olduğumda mevcutte bulunduğum adresi tutup giriş yapınca yine o adrese yönlendirmesi için ?ReturnUrl= " + encodeURIComponent(window.location.pathname) kısmını yazdık.
      return new Promise(() => {}); // normalde dönmen gereken değeri dön ama boş olarak dön.
    }

    if (statusCode === 403) {
      router.push({
        name: "forbidden"
      });
      return new Promise(() => {});
    }

    return Promise.reject(error);
  }
);