import axios from "axios";
import router from "@/router";
import Vue from 'vue';

export const http = axios.create({
  headers: {
    "Content-Type": "application/json",
    "X-Requested-With": "XMLHttpRequest",
    "X-Application-Name": "vue"
  }
});

http.interceptors.response.use(
  function (response) {
	if(response.data ){
		if(response.data.success && response.data.message){
			Vue.notify({
			title: 'Success',
			text: response.data.message
			})
		}
	}
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
	const response = error.response;
	if(!response.data.success && response.data.message){
		Vue.notify({
			title: 'Error',
			text: response.data.message,
			type: 'error'
			})
	}
    return Promise.reject(error);
  }
);