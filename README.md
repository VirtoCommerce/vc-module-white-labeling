# Virto Commerce White Labeling Module

The White Labeling module enables organizations to enhance the enterprise user experience by customizing branding elements such as logos, favicons, colors, and footer links based on the logged-in organization context. This module ensures that users from different organizations see personalized branding elements after signing in, improving overall UX and brand consistency.

## Key features

* **Branding Configuration**: Display organization-specific logos, favicons, colors, and footer links post-login.
* **White labeling**: Resolve branding based on the organization context after user authentication.
* **Integration with Virto Frontend:** Seamlessly integrate customized branding elements into Virto Commerce Storefront.
* **Custom Domain:** Assign a custom domain with the organization to allow branding activation on the first visit.
* **Change My Organization Logo:** Organization maintainers can update their organization's logo through a self-service option.. 
* **Automated FavIcon Generation**: The module uses [Virto Commerce Image Tools module](https://github.com/VirtoCommerce/vc-module-image-tools/) for fav icon generation.

## Screenshots

![Demo](https://github.com/VirtoCommerce/vc-module-white-labeling/assets/7639413/0fab3f2e-53ef-47ca-802d-45ce55ecfa24)

![Store Settings](https://github.com/VirtoCommerce/vc-module-white-labeling/assets/7639413/179f9cf7-d993-46a1-90e9-89c034c4e9ed)

![Organization White labeling](https://github.com/VirtoCommerce/vc-module-white-labeling/assets/7639413/2af4e983-30f3-4e3c-8597-914b9d48a537)

## Configuration
A file upload scope named organization-logo should be defined in the platform settings to have an option to change my organization logo from the frontend. 
For more details, [refer to the documentation](https://github.com/VirtoCommerce/vc-module-file-experience-api?tab=readme-ov-file#getting-started).

To register the upload scope, update appsettings.json with the following file upload scope settings:

```json
{
  "FileUpload": {
    "Scopes": [
      {
        "Scope": "organization-logos",
        "MaxFileSize": 10485760,
        "AllowedExtensions": [
          ".png"
        ]
      }
    ]
  }
}
```

## Integration with Virto Frontend

Virto Frontend has native integration with the White Labeling module. 
Once you install the module and click Activate in Store Settings, the White Labeling feature will be activated.

## Documentation

* [White Labeling module user documentation](https://docs.virtocommerce.org/platform/user-guide/white-labeling/overview/)
* [REST API](https://docs.virtocommerce.org/platform/developer-guide/Fundamentals/Taxes/new-tax-provider-registration/)
* [View on GitHub](https://github.com/VirtoCommerce/vc-module-white-labeling)


## References

* [Deployment](https://docs.virtocommerce.org/platform/developer-guide/Tutorials-and-How-tos/Tutorials/deploy-module-from-source-code/)
* [Installation](https://docs.virtocommerce.org/platform/user-guide/modules-installation/)
* [Home](https://virtocommerce.com)
* [Community](https://www.virtocommerce.org)
* [Download latest release](https://github.com/VirtoCommerce/vc-module-white-labeling/releases/latest)

## License
Copyright (c) Virto Solutions LTD.  All rights reserved.

This software is licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at http://virtocommerce.com/opensourcelicense.

Unless required by the applicable law or agreed to in written form, the software
distributed under the License is provided on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
