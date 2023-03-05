
# ASP.NET Core Issue and Verify Azure AD Verifiable Credentials 

[![.NET](https://github.com/swiss-ssi-group/AzureADVerifiableCredentialsAspNetCore/actions/workflows/dotnet.yml/badge.svg)](https://github.com/swiss-ssi-group/AzureADVerifiableCredentialsAspNetCore/actions/workflows/dotnet.yml)

## Blogs

- [Getting started with Self Sovereign Identity SSI](https://damienbod.com/2021/03/29/getting-started-with-self-sovereign-identity-ssi/)
- [Challenges to Self Sovereign Identity](https://damienbod.com/2021/10/11/challenges-to-self-sovereign-identity/)
- [Create and issue verifiable credentials in ASP.NET Core using Azure AD](https://damienbod.com/2021/10/25/create-and-issuer-verifiable-credentials-in-asp-net-core-using-azure-ad/)

## History

2023-03-05 Fixed new VC payloads, fixed cache, recreated all VCs

2023-03-03 Updated to .NET 7, Update AAD VC service with all the breaking changes

2022-03-18 Updated code 

2021-11-12 Updated to .NET 6 release

## User secrets Issuer, Verify

```
{
  "CredentialSettings": {
    "Endpoint": "https://beta.did.msidentity.com/v1.0/{0}/verifiablecredentials/request",
    "VCServiceScope": "bbb94529-53a3-4be5-a069-7eaf2712b826/.default",
    "Instance": "https://login.microsoftonline.com/{0}",
    "TenantId": "YOURTENANTID",
    "ClientId": "APPLICATION CLIENT ID",
    "VcApiCallbackApiKey": "SECRET",
    "Authority": "YOUR authority",
    "ClientSecret": "[client secret or instead use the prefered certificate in the next entry]",
    // "CertificateName": "[Or instead of client secret: Enter here the name of a certificate (from the user cert store) as registered with your application]",
    "IssuerAuthority": "YOUR VC SERVICE DID",
    "VerifierAuthority": "YOUR VC SERVICE DID",
    "CredentialManifest":  "THE CREDENTIAL URL FROM THE VC PORTAL"
  }
}

```

## Feedback in issuer app

When running the issuer application, ngrok is used if you would like to receive feedback from the VC issuing through the callback. This requires a public IP. The IP needs to be added to the **Azure App Registration** as a redirect URL to authenticate. ngrok is only used for development.

## CredentialsClaims

The **CredentialsClaims** classes in both the issuer and the verifier are used to add the specific claims to the definition of the Azure VC credential.

The classes must match the definitions. By changing these classes and the rules fields, different Azure verifiable credentials can be used.

## GetIssuanceRequestPayloadAsync

This method defines the specifics of the issue request payload. This would need to be changed, if you required no pin verification or other flows. See the Azure AD VC docs for more info.

## Microsoft sample APP demo-code-from-microsoft-sample

The **demo-code-from-microsoft-sample** is a second sample created directly from the Azure [sample](https://github.com/Azure-Samples/active-directory-verifiable-credentials-dotnet)

This has only configuration changes for my tenant and VC definitions. Please refer to the sample repo for more info about this.

## Links

https://docs.microsoft.com/en-us/azure/active-directory/verifiable-credentials/

https://docs.microsoft.com/en-us/azure/active-directory/verifiable-credentials/get-started-request-api

https://github.com/Azure-Samples/active-directory-verifiable-credentials-dotnet

https://www.microsoft.com/de-ch/security/business/identity-access-management/decentralized-identity-blockchain

https://didproject.azurewebsites.net/docs/issuer-setup.html

https://didproject.azurewebsites.net/docs/credential-design.html

https://github.com/Azure-Samples/active-directory-verifiable-credentials

https://identity.foundation/

https://www.w3.org/TR/vc-data-model/

https://daniel-krzyczkowski.github.io/Azure-AD-Verifiable-Credentials-Intro/

https://dotnetthoughts.net/using-node-services-in-aspnet-core/

https://identity.foundation/ion/explorer

https://www.npmjs.com/package/ngrok

https://github.com/microsoft/VerifiableCredentials-Verification-SDK-Typescript

https://identity.foundation/ion/explorer

https://www.npmjs.com/package/ngrok

https://github.com/microsoft/VerifiableCredentials-Verification-SDK-Typescript
