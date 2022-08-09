<script lang="ts" setup>
import TreeView from "@grapoza/vue-tree" // https://grapoza.github.io/vue-tree/4.0.3/demos.html#asynchronous-loading
// const url = 'https://rommaster-api.wally.best/users'
const url = 'https://localhost:7181/paths'

const loadNodesAsync = async () => {
    const response = await $fetch(url);
    return (response as any).items;
}

const loadChildrenAsync = async (parentModel) => {
  const response = await $fetch(`${url}/${parentModel.id}`);
  return (response as any).items;
};

const addChildCallback = (parentModel) => console.log("ADD", parentModel);

const modelDefaults = {
  loadChildrenAsync: loadChildrenAsync,
  addChildCallback: addChildCallback,
  addChildTitle: 'Add a new child node',
  deleteTitle: 'Delete this node',
  expanderTitle: 'Expand this node',
  selectable: true,
  idProperty: 'id',
  labelProperty: 'name',
  // expandable: false,
  /*state: {
    expanded: true
  },*/
  customizations: {
    classes: {
      treeViewNodeSelf: 'large-line',
      treeViewNodeSelfText: 'big-text',
      treeViewNodeSelfExpander: 'action-button',
      treeViewNodeSelfExpandedIndicator: 'fa-solid fa-chevron-right',
      treeViewNodeSelfAction: 'action-button',
      treeViewNodeSelfAddChildIcon: 'fa-solid fa-plus-circle',
      treeViewNodeSelfDeleteIcon: 'fa-solid fa-minus-circle'
    }
  }
};
</script>

<template>
  <client-only placeholder="loading...">

    <font-awesome-icon icon="fa-solid fa-chevron-right" />
    <font-awesome-icon :icon="['fas', 'home']" />
    <font-awesome-icon :icon="['fas', 'user-secret']" />
    <font-awesome-icon icon="fa-solid fa-plus-circle" />
    <font-awesome-icon icon="fa-solid fa-minus-circle" />

    <!-- solid style -->
    <i class="fa-solid fa-user"></i>

    <!-- regular style -->
    <i class="fa-regular fa-user"></i>

    <!-- light style -->
    <i class="fa-light fa-user"></i>

    <!-- duotone style -->
    <i class="fa-duotone fa-user"></i>

    <!-- all new thin style -->
    <i class="fa-thin fa-user"></i>

    <!--brand icon-->
    <i class="fa-brands fa-github-square"></i>
    
    <div>
      <tree-view :load-nodes-async="loadNodesAsync" :model-defaults="modelDefaults" selection-mode="single" :skin-class="'grtv grayscale'">
        <template #text="{ model, customClasses }">
          <code>
            [{{ model[model.treeNodeSpec.labelProperty] }}]
          </code>
        </template>
        <template #checkbox="{ model, customClasses, inputId, checkboxChangeHandler }">
          <code>{{ "Slotted Content for " + model[model.treeNodeSpec.labelProperty] }}</code>
        </template>
        <template v-slot:radio="{ model, customClasses, inputId, inputModel, radioChangeHandler }">
          <div>RADIO</div>
        </template>
        <template v-slot:loading="{ model, customClasses }">
          <div>...</div>
        </template>
        <template v-slot:loading-root>
          LOADING...
        </template>
      </tree-view>
    </div>
  </client-only>
</template>

<style>
@import '@/assets/css/main.css';

ul.grtv {
  list-style-type: none;
}
</style>
