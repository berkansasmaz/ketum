<template>
	<div>
    <span v-b-tooltip :title="title" :class="'badge badge-'+badgeClass">{{title.substr(0,1)}}</span>	</div>
</template>>

<script>
export default {
	props:{
		title: {
			"default": "Unknown",
			"type" : String
		},
		status: {
			"default": 0,
			"type" : Number
		},
	},
	computed: {
		badgeClass(){
			switch (this.status) {
				case 0:
					return "dark";
				case 1:
					return "info";
				case 2:
					return "primary";
				case 3:
					return "success";
				case 4:
					return "warning";
				case 5:
					return "danger";
				case 6:
					return "dark";
				default:
					return "light";
			}
		}
	},
}
</script>
