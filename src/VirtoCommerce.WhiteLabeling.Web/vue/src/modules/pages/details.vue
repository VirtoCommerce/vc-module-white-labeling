<template>
  <VcBlade
    :title="$t('WHITE_LABELING.TITLE')"
    :toolbar-items="bladeToolbar"
    :loading="loading"
    width="50%"
  >
    <VcContainer>
      <VcForm class="white-labeling-form tw-space-y-4">
        <VcCard :header="$t('WHITE_LABELING.GENERAL')">
          <div class="white-labeling-card">
            <VcSwitch
              v-model="item.isEnabled"
              :label="$t('WHITE_LABELING.ENABLED')"
            />
            <VcSelect
              v-model="item.themePresetName"
              :label="$t('WHITE_LABELING.THEME')"
              :options="themes"
            />
            <VcRow class="tw-gap-4">
              <VcCol>
                <VcInput
                  v-model="item.mainMenuLinkListName"
                  :label="$t('WHITE_LABELING.MAIN_MENU')"
                />
              </VcCol>
              <VcCol>
                <VcInput
                  v-model="item.footerLinkListName"
                  :label="$t('WHITE_LABELING.FOOTER')"
                />
              </VcCol>
            </VcRow>
          </div>
        </VcCard>
        <VcCard :header="$t('WHITE_LABELING.IMAGES')">
          <div class="white-labeling-card">
            <VcRow class="tw-gap-4">
              <VcCol>
                <ImageField
                  v-model="item.logoUrl"
                  :label="$t('WHITE_LABELING.LOGO')"
                  folder="/customization"
                  :file-name="`logo_${ownerId}`"
                  accept=".png,.gif,.svg"
                />
              </VcCol>
              <VcCol>
                <ImageField
                  v-model="item.secondaryLogoUrl"
                  :label="$t('WHITE_LABELING.SECONDARY_LOGO')"
                  folder="/customization"
                  :file-name="`secondary_logo_${ownerId}`"
                  accept=".png,.gif,.svg"
                />
              </VcCol>
            </VcRow>
            <ImageField
              v-model="item.faviconUrl"
              :label="$t('WHITE_LABELING.FAVICON')"
              folder="/customization/favicons"
              :file-name="`favicon_${ownerId}`"
              accept=".png,.jpg,.jpeg,.webp"
            />
          </div>
        </VcCard>
      </VcForm>
    </VcContainer>
  </VcBlade>
</template>

<script setup lang="ts">
import { useApiClient, useAsync, useBlade, useBladeForm, useLoading, type IBladeToolbar } from "@vc-shell/framework";
import { VcBlade, VcCard, VcCol, VcContainer, VcForm, VcInput, VcRow, VcSelect, VcSwitch } from "@vc-shell/framework/ui";
import { computed, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { WhiteLabelingClient, type WhiteLabelingSetting } from "../../api_client/virtocommerce.whitelabeling";
import { api, toOptions, type Option } from "../api";
import ImageField from "../components/ImageField.vue";

// White labeling of a store or of an organization (vc-module-white-labeling): one setting per owner
defineBlade({
  url: "/white-labeling",
  name: "WhiteLabelingDetails",
  // its owner comes only in options, which a reload doesn't bring back
  routable: false,
});

const { t } = useI18n({ useScope: "global" });
const { options } = useBlade<{ storeId?: string; organizationId?: string }>();
const { getApiClient } = useApiClient(WhiteLabelingClient);

const ownerId = computed(() => options.value?.storeId ?? options.value?.organizationId ?? "");
const item = ref<WhiteLabelingSetting>({});
const themes = ref<Option[]>([]);

const form = useBladeForm({ data: item, closeConfirmMessage: computed(() => t("WHITE_LABELING.ALERTS.CLOSE_CONFIRMATION")) });

const { loading: fetching, action: load } = useAsync(async () => {
  const client = await getApiClient();
  const { storeId, organizationId } = options.value ?? {};
  if (!storeId && !organizationId) return;
  // the API answers an empty body (null) when the owner has no setting yet; an error is not "no setting", it fails the
  // load and Save stays off
  const existing = await (storeId ? client.getByStoreId(storeId) : client.getByOrganizationId(organizationId!));
  item.value = existing?.id ? existing : { isEnabled: false, storeId, organizationId };
  form.setBaseline();
});

const { loading: saving, action: save } = useAsync(async () => {
  const client = await getApiClient();
  // a cleared field is "not set" (null), not an empty string
  const setting = Object.fromEntries(Object.entries(item.value).map(([k, v]) => [k, v === "" ? null : v])) as WhiteLabelingSetting;
  if (setting.id) await client.update(setting);
  else await client.create(setting);
  await load();
});

const loading = useLoading(fetching, saving);

const bladeToolbar = ref<IBladeToolbar[]>([
  {
    id: "save",
    title: t("WHITE_LABELING.TOOLBAR.SAVE"),
    icon: "lucide-save",
    permissions: "WhiteLabeling:update",
    disabled: computed(() => !form.canSave.value),
    clickHandler: () => save(),
  },
  {
    id: "reset",
    title: t("WHITE_LABELING.TOOLBAR.RESET"),
    icon: "lucide-undo-2",
    disabled: computed(() => !form.isModified.value),
    clickHandler: () => form.revert(),
  },
]);

onMounted(() => {
  // as legacy va-setting-value-select: the values endpoint, which needs no setting permission
  api
    .get<unknown[]>("/api/platform/settings/values/WhiteLabeling.ThemePresetNames")
    .catch(() => undefined)
    .then((values) => (themes.value = toOptions(values)));
  load();
});
</script>

<style lang="scss">
.white-labeling-form {
  padding: 20px 22px 36px;
}

.white-labeling-card {
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding: 16px;
}
</style>
