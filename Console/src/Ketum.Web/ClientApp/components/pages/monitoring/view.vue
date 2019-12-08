<template>
  <div>
	<template v-if="!item">
		<div>
			<content-placeholders>
			<content-placeholders-heading :img="true" />
			<content-placeholders-text :lines="15" />
			</content-placeholders>
		</div>
	</template>
    <template v-if="item">
		<page-head prefix="berkansasmaz.com" title="Dashboard"></page-head>
	<div class="row">
	<div class="col-sm-6 col-xl-3">
	
	<div class="card mb-4">
	<div class="card-body">
	<div class="d-flex align-items-center">
	<div class="lnr lnr-cart display-4 text-success"></div>
	<div class="ml-3">
	<div class="text-muted small">Monthly sales</div>
	<div class="text-large">2362</div>
	</div>
	</div>
	</div>
	</div>
	
	</div>
	<div class="col-sm-6 col-xl-3">
	
	<div class="card mb-4">
	<div class="card-body">
	<div class="d-flex align-items-center">
	<div class="lnr lnr-earth display-4 text-info"></div>
	<div class="ml-3">
	<div class="text-muted small">Monthly visits</div>
	<div class="text-large">687,123</div>
	</div>
	</div>
	</div>
	</div>
	
	</div>
	<div class="col-sm-6 col-xl-3">
	
	<div class="card mb-4">
	<div class="card-body">
	<div class="d-flex align-items-center">
	<div class="lnr lnr-gift display-4 text-danger"></div>
	<div class="ml-3">
	<div class="text-muted small">Products</div>
	<div class="text-large">985</div>
	</div>
	</div>
	</div>
	</div>
	
	</div>
	<div class="col-sm-6 col-xl-3">
	
	<div class="card mb-4">
	<div class="card-body">
	<div class="d-flex align-items-center">
	<div class="lnr lnr-users display-4 text-warning"></div>
	<div class="ml-3">
	<div class="text-muted small">Users</div>
	<div class="text-large">105,652</div>
	</div>
	</div>
	</div>
	</div>
	</div>
	</div>
	<div class="row">
	<div class="col-md-12">
	<div class="card mb-4">
	<h6 class="card-header with-elements">
	<div class="card-header-title">Sale stats</div>
	<div class="card-header-elements ml-auto d-none">
	<button type="button" class="btn btn-default btn-xs md-btn-flat">Show more</button>
	</div>
	</h6>
	<div class="table-responsive">
	<table class="table card-table">
	<thead>
	<tr>
	<th>Date</th>
	<th>Sales</th>
	<th>Cancelled</th>
	<th>Delivered</th>
	</tr>
	</thead>
	<tbody>
	<tr>
	<td>02/08/2018</td>
	<td>12</td>
	<td>1</td>
	<td>5</td>
	</tr>
	<tr>
	<td>02/07/2018</td>
	<td>16</td>
	<td>2</td>
	<td>8</td>
	</tr>
	<tr>
	<td>02/06/2018</td>
	<td>5</td>
	<td>0</td>
	<td>2</td>
	</tr>
	<tr>
	<td>02/05/2018</td>
	<td>21</td>
	<td>1</td>
	<td>10</td>
	</tr>
	</tbody>
	</table>
	</div>
	</div>
	</div>
	</div>
	</template>
  </div>
</template>

<script>
  import service from "service/monitoring";
  export default {
    data() {
      return {
        id: null,
        item: null
      }
    },
    async mounted() {
		var result = await service.get(this.$route.params.id);
		if (result.success) {
			setTimeout(() => {
				this.item = result.data;
			}, 3000);
		}
	}
};
</script>
