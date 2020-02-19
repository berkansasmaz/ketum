import {
  http
} from "utils/http";

const SubscriptionService = {
  async list() {
    var result = await http.get("/api/v1/subscription");
    if (result.status === 200) {
      return result.data;
    } else {
      console.error(result.error);
      throw result.error;
    }
  },
  async current() {
    var result = await http.get("/api/v1/subscription/current");
    if (result.status === 200) {
      return result.data;
    } else {
      console.error(result.error);
      throw result.error;
    }
  },
  async subscribe(id) {
    var result = await http.post("/api/v1/subscription/" + id);
    if (result.status === 200) {
      return result.data;
    } else {
      console.error(result.error);
      throw result.error;
    }
  }
};

export default SubscriptionService;
