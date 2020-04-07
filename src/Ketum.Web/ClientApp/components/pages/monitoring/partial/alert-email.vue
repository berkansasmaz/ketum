<template>
  <b-form-group
    description="This field will define the email address for alerting email"
    label="Alerting email address"
    label-for="email"
  >
    <b-form-input
      id="email"
      placeholder="Type your alerting email"
      trim
      v-model="email"
      v-on:input="$emit('input', model)"
    />
  </b-form-group>
</template>
<script>
  export default {
    props: {
      value: {
        default: {},
        type: Object
      }
    },
    computed: {
      model() {
        return {
          email: this.email
        };
      }
    },
    data() {
      return {
        email: ""
      };
    }
  };
</script>
