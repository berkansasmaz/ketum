<template>
  <div>
    <template>
      <page-head prefix="Subscription" title="Free"/>

      <!-- Content -->
      <div class="container-fluid flex-grow-1 container-p-y">

        <div class="container">

          <h1 class="text-center font-weight-bolder">
            Pricing plans
          </h1>
          <div class="text-center text-muted text-large font-weight-light">
            Choose the best plan that fits your needs
          </div>

          <div class="d-flex align-items-center my-5">
            <div class="flex-shrink-1 w-100 text-right text-big mr-3">Billed monthly</div>
            <label class="switcher switcher-lg switcher-success m-0">
              <input type="checkbox" class="switcher-input" checked>
              <span class="switcher-indicator">
                    <span class="switcher-yes"></span>
                    <span class="switcher-no"></span>
                  </span>
            </label>
            <div class="flex-shrink-1 w-100 text-success text-big ml-3">Billed annually - Save 20%</div>
          </div>


          <div class="card mb-4" v-if="subscription">
            <div class="card-body">
              <h4 class="card-title">
                <strong>Free</strong>
              </h4>
              <div class="card-subtitle text-muted mb-3">Current subscription <small>({{subscription.paymentPeriodText}})</small></div>
              <ul>
                <li v-for="(item, index) in subscription.features" :key="'feature-' + index">
                  {{item.valueUsed}}/{{item.value}} {{item.title}}
                </li>
              </ul>
            </div>
          </div>

          <div class="row no-gutters row-bordered ui-bordered text-center">

            <div v-for="(item, index) in subscriptions" :key="'subscription' + item.id" class="d-flex col-md flex-column py-4">
              <div class="display-1 text-primary my-4"><icon icon="briefcase"></icon></div>
              <h5 class="m-0">{{item.title}}</h5>
              <div class="flex-grow-1">
                <div class="py-4 my-2">
                  <span class="d-inline-block text-muted text-big align-middle mr-2">$</span>
                  <span class="display-3 d-inline-block font-weight-bold align-middle">{{item.price}}</span>
                  <span class="d-inline-block text-muted text-big align-middle ml-2">/ mo</span>
                </div>
                <div class="pb-5">
                  <p v-for="(feature, index) in item.features" :key="item.id + '-' + index" class="ui-company-text mb-2">
                    {{feature.value}} {{feature.title}}
                  </p>

                </div>
              </div>
              <div class="px-md-3 px-lg-5" v-if="subscription == null || item.id != subscription.typeId">
                <button @click="subscribe(item)" class="btn btn-outline-success"><icon icon="credit-card"></icon> Subscribe</button>
              </div>
              <div class="px-md-3 px-lg-5" v-else>
                <p class="text-success text-center">You already have this subscription</p>
              </div>
            </div>

          </div>

          <div class="text-center text-big mt-5">
            <a href="javascript:void(0)">Get started with a 14-day no-obligation free trial</a>
          </div>

          <hr class="border-light mt-5">

        </div>
      </div>
      <!-- / Content -->
    </template>
    <vue-stripe-checkout
      ref="checkoutRef"
      :name="selectedSubscription.title"
      :description="selectedSubscription.description"
      :email="selectedSubscription.email"
      currency="usd"
      :amount="selectedSubscription.price"
      :allow-remember-me="false"
      @done="paymentDone"
      @canceled="paymentCanceled"
    />
  </div>
</template>

<script>
import service from "service/subscription";
export default {
  data() {
    return {
      subscription: null,
      selectedSubscription: {
        id: "",
        price: 0,
        description: "",
        title: "",
        email: "berkansasmazz@gmail.com"
      },
      subscriptions: []
    };
  },
  async mounted() {
    const result = await service.list();
    this.subscriptions = result.data;
    await this.current();
  },
  methods: {
    async subscribe(subscription) {
      this.selectedSubscription.id = subscription.id;
      this.selectedSubscription.title = subscription.title;
      this.selectedSubscription.price = subscription.price * 100;
      this.selectedSubscription.description = subscription.description;
      setTimeout(async () => {
        const { token, args } = await this.$refs.checkoutRef.open();
      }, 50);
    },
    async current() {
      const current = await service.current();
      this.subscription = current.data;
    },
    async paymentDone({ token, args }) {
      console.log(token);
      console.log(args);
      const result = await service.subscribe(
        this.selectedSubscription.id,
        token.id
      );
      if (result.data) {
        await this.current();
      }
    },
    paymentCanceled() {
      // do stuff
    }
  }
};
</script>
