import * as blades from "./pages";
import * as locales from "./locales";
import { defineAppModule, registerExternalWidget } from "@vc-shell/framework";
import { markRaw } from "vue";
import WhiteLabelingWidget from "./widgets/WhiteLabelingWidget.vue";

// The AngularJS module's widget on the store blade ("storeDetail") and the organization blade ("organizationDetail2"):
// one white labeling setting per owner. Hidden on an owner that isn't saved yet.
registerExternalWidget({
  id: "WhiteLabeling",
  title: "White labeling",
  component: markRaw(WhiteLabelingWidget),
  targetBlades: ["StoreDetails", "CustomerOrganizationDetails"],
  isVisible: (descriptor) => !!descriptor?.param,
});

export default defineAppModule({
  blades,
  locales,
});
