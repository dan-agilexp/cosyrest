# Description

CosyRest is a NuGet package which provides a wrapper around the non-advertised REST API for the Geo Cosy Smart Thermostat.

Geo Cosy do not advertise a public API for interacting with their smart thermostats. However by inspecting the API calls made by the [Geo Cosy Web App](https://cosy.geotogether.com/) it is easy to replicate the calls using cURL to obtain a token and then perform operations on the smart thermostat.

This NuGet package wraps up those API calls to provide a simple Geo Cosy client.

Warning: As the Geo Cosy API is not advertised publicly, it is subject to change at any time by them without any prior notification. Therefore if you want to control the Geo Cosy thermostats for any critial or commercial application I suggest you get in touch with them directly. However, presuming their hardware (the Geo Cosy smart thermostat and Cosy hub uses these same API calls, I would say its unlikely to change much while the product is supported.

# Motivation

I wanted to control my heating with automation for example to "make it cosy" one hour before a scheduled AirBnB check-in.
Although GeoCosy has an Alexa skill there is nothing similar for IFTTT or a public API so I set about building my own.
The inspiration came from [this GitHub Repo](https://github.com/Saggerus/HA-cosy-server) but using a headless browser to simulate clicking around the web interface wasn't quite what I was after and plus I wanted a do it using .NET.
However the project above might better suit your needs to worth checking out.

# Installation

To install this package into your .NET 5 application, use the following command:

`Install-Package AgileXp.CosyRest`

... or search "Cosy" in NuGet package manager.

# Setup instructions

1. Install `AgileXp.CosyRest` NuGet Package
2. Add the following lines to your Startup method in configure services

3. Inject IGeoCosyClient.

```
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            services.AddScoped<IGeoCosyClient, GeoCosyClient>();
            services.AddHttpClient();
            ...
        }
```

# Usage

## Obtain a JWT token:

```
var token = await cosyClient.LoginAsync("<your geo cosy web/app email address>", "<your geo cosy web/app password>");
```

## Get a list of Geo Cosy devices registered to your account

```
var devices = await cosyClient.GetSystems(token.Data.Token, true);
```

## Set a device to Cosy for two hours
```
 await cosyClient.SetMode(token.Data.Token, devices.Data.SystemDetails[0].SystemId, CosyMode.Cosy, 120);
```

## Set a device to Hibernate
```
 await cosyClient.SetMode(token.Data.Token, devices.Data.SystemDetails[0].SystemId, CosyMode.Hibernate);
```
or
```
 await cosyClient.Hibernate(token.Data.Token);
```

# Contributions

Feel free to submit a pull request to `master` and once merged, I will update the NuGet package. If I get a few merge requests I'll set up some CI to do this.

# Possible extensions

- Pub/Sub: Subscribe to ping messages from the Geo Cosy API and create an event handler mechanism to respond to changes of status.
