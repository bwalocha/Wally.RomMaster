<!--https://grapoza.github.io/vue-tree/4.0.3/demos.html#asynchronous-loading-->
<script lang="ts" setup>

import { ref } from '@vue/reactivity';
import TreeView from "@grapoza/vue-tree"
// const url = 'https://rommaster-api.wally.best/users'
const url = 'https://localhost:7181/paths'
// const { data, pending, refresh, error } = await useFetch(url);

// const items = ref<any[]>([]); // define your tree items here.
/*
const selectedItems = ref<TreeViewItem[]>([]);
const checkedItems = ref<TreeViewItem[]>([]);
const selectedItem = ref<TreeViewItem>();
*/

// const onItemChecked = (arg: ItemEventArgs) => console.log(arg.item.name, arg.change);
// const onItemSelected = (arg: ItemEventArgs) => console.log(arg.item.name);

/*watch(data, (d) => {
  // Because posts starts out null, you won't have access
  // to its contents immediately, but you can watch it.
  items.value = (d as any).items.map(a => ({ id: a.id, label: a.name, children: [] }));
})*/

/*const loadNodesAsync = async () => {
  return new Promise(resolve => setTimeout(resolve.bind(null, [
    {
      id: "async-rootnode",
      label: "Root Node"
    }
  ]), 1000));
}*/

const loadNodesAsync = async () => {
  const x = [
    {
      id: "async-rootnode",
      label: "Root Node"
    }
  ]
  // const { data, pending, refresh, error } = await useFetch(url).then(a => a.data);

  return useFetch(url)
    .then(a => {
      console.log(a);
      
      const z = (a.data.value as any)
      ?.items
      ?.map(a => ({ id: a.id, label: a.name, children: [] }))
    
      return z;
    });
}

const loadChildrenAsync = async (parentModel) => {
  const id = Date.now();
  return new Promise(resolve => setTimeout(resolve.bind(null, [
    {
      id: `async-child-node-${id}-1`,
      label: `Child ${id}-1`
    },
    {
      id: `async-child-node-${id}-2`,
      label: `Child ${id}-2`,
      treeNodeSpec: { deletable: true }
    }
  ]), 1000));
};

const modelDefaults = ref<any>({
  loadChildrenAsync: loadChildrenAsync,
  deleteTitle: 'Delete this node',
  expanderTitle: 'Expand this node'
});

</script>

<template>
  <client-only placeholder="loading...">
<!--  <div>
    error: <code>{{ error }}</code>
    Items: <code>{{ items }}</code>
  </div>-->
  <div>

    <div>
<!--      <tree-view v-if="error === null && pending === false" :load-nodes-async="loadNodesAsync" :model-defaults="modelDefaults">
      </tree-view>-->
      <tree-view :load-nodes-async="loadNodesAsync" :model-defaults="modelDefaults">
      </tree-view>
    </div>
    
    <div>
<!--      <button :disabled="pending === true" @click="refresh">
        refresh
      </button>-->
    </div>

  </div>
  </client-only>
 
</template>

<style scoped>
</style>
