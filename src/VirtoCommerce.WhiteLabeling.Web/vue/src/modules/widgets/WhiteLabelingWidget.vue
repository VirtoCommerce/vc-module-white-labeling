<template>
  <VcWidget
    icon="lucide-palette"
    :title="$t('WHITE_LABELING.TITLE')"
    :disabled="!ownerId"
    @click="open"
  />
</template>

<script setup lang="ts">
import { computed } from "vue";
import { injectBladeContext, useBlade } from "@vc-shell/framework";
import { VcWidget } from "@vc-shell/framework/ui";

// the store or organization shared by its blade via defineBladeContext({ item }); the host blade's name tells which
const ctx = injectBladeContext();
const ownerId = computed(() => (ctx.value.item as { id?: string } | undefined)?.id);
const { name, openBlade } = useBlade();

const open = () =>
  openBlade({
    name: "WhiteLabelingDetails",
    param: ownerId.value,
    options: name.value === "StoreDetails" ? { storeId: ownerId.value } : { organizationId: ownerId.value },
  });
</script>
