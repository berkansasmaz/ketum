import { http } from 'utils/http';

const MonitoringService = {
	async list () {
		var result = await http.get('/api/v1/monitoring');

		if (result.status == 200) {
			console.log(result);
		} else {
			console.error('result.error');
			throw result.error;
		}

		return result.data;
	},

	async save (value) {
		var result = await http.post('/api/v1/monitoring', value);

		if (result.status == 200) {
			console.log(result);
		} else {
			console.error('result.error');
			throw result.error;
		}

		return result.data;
	}
};

export default MonitoringService;
