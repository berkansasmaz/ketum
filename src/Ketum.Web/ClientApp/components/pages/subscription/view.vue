<template>
  <div>
    <div>
      <div class="card mb-4" v-if="subscription">
        <div class="card-body">
          <h4 class="card-title">
            <strong>{{ subscription.title }}</strong>
          </h4>
          <div
            class="card-subtitle text-muted mb-3"
          >Current subscription is {{ subscription.title }} ({{subscription.paymentPeriodText}})</div>
          <ul>
            <li
              v-for="(f, index) in subscription.features"
              :key="'feature-'+index"
            >
              <span v-if="f.valueUsed && f.valueRemained">
                {{f.valueUsed}}/{{f.value}} {{ f.title }}
              </span>
              <span v-else>
                {{f.value}} {{f.title}}
              </span>
            </li>
          </ul>
        </div>
      </div>
      <div class="row no-gutters row-bordered ui-bordered text-center">
        <div
          v-for="item in subscriptions"
          :key="'subscription'+item.id"
          class="d-flex col-md flex-column py-4"
        >
          <div class="display-1 text-primary my-4">
            <i class="lnr lnr-briefcase text-big"></i>
          </div>
          <h5 class="m-0">{{ item.title }}</h5>
          <div class="flex-grow-1">
            <div class="py-4 my-2">
              <span class="d-inline-block text-muted text-big align-middle mr-2">$</span>
              <span class="display-3 d-inline-block font-weight-bold align-middle">{{ item.price }}</span>
              <span class="d-inline-block text-muted text-big align-middle ml-2">/ mo</span>
            </div>
            <div class="pb-5">
              <p
                v-for="(feature,index) in item.features"
                :key="item.id+'-'+index"
                class="ui-company-text mb-2"
              >{{feature.value}} {{feature.title}}</p>
            </div>
          </div>
          <div
            class="px-md-3 px-lg-5"
            v-if="subscription == null || item.id != subscription.typeId"
          >
            <button @click="subscribe(item)" class="btn btn-outline-success">
              <icon icon="credit-card"/>Subscribe
            </button>
          </div>
          <div class="px-md-3 px-lg-5" v-else>
            <p class="text-success">You already have this subscription</p>
          </div>
        </div>
      </div>
    </div>
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
          email: "berkansasmaz@gmail.com"
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
        }, 100);
      },
      async current() {
        const current = await service.current();
        this.subscription = current.data;
      },
      async paymentDone({ token, args }) {
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
