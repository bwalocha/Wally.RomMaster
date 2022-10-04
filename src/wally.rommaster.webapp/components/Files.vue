<script lang="ts" setup>
  const _onBeforeMount = onBeforeMount(() => console.log("Before Mount!", props.directoryId));

  const props = defineProps({
    directoryId: String
  })

//   const emit = defineEmits(['change', 'delete'])
</script>

<template>
  Files:
</template>

<style scoped>

</style>
