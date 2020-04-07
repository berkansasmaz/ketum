import {
  http
} from "utils/http";

const MonitorAlertService = {
  async list(id) {
    var result = await http.get(`/api/v1/monitoralert/list/${id}`);
    if (result.status === 200) {
      return result.data;
    } else {
      console.error(result.error);
      throw result.error;
    }
  },

  async get(id) {
    var result = await http.get("/api/v1/monitoralert/" + id);
    if (result.status === 200) {
      return result.data;
    } else {
      console.error(result.error);
      throw result.error;
    }
  },

  // async logs(id, page) {
  //   if (!page) {
  //     page = 0;
  //   }
  //   var result = await http.get(`/api/v1/monitoralert/logs/${id}?page=${page}`);
  //   if (result.status === 200) {
  //     return result.data;
  //   } else {
  //     console.error(result.error);
  //     throw result.error;
  //   }
  // },

  async save(value) {
    var result = await http.post("/api/v1/monitoralert", value);
    if (result.status === 200) {
      return result.data;
    } else {
      console.error(result.error);
      throw result.error;
    }
  }
}

export default MonitorAlertService;
