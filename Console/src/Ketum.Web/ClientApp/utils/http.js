import axios from 'axios';
 import router 	from '@/router';
export const  http = axios.create({
	headers: {
		"Content-Type": "application/json",
		"X-Requested-With": "XMLHttpRequest",
		"X-Application-Name": "vue"
	}
});

http.interceptors.response.use(
		function( response ){
		return response
		}, 
		function(error){
			const statusCode = error.response.status;
			if (statusCode == 401) {
				window.location.href = '/Identity/Account/Login?ReturnUrl=' + encodeURIComponent(window.location.pathname);
				return new Promise(()	=>	{});
			} 

			if (statusCode == 403) {
				router.push({
					name: "forbidden"
				});
				return new Promise(()	=>	{});
			}

			return Promise.reject(error);
		}
);
