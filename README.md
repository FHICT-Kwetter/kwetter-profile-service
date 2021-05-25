# Profile Service

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-orange.svg)](https://sonarcloud.io/dashboard?id=FHICT-Kwetter_kwetter-identity)

![build](https://github.com/FHICT-Kwetter/kwetter-identity/workflows/pipeline/badge.svg)
![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=coverage)
![Maintainability](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=sqale_rating)
![Reliability](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=reliability_rating)
![Security](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=security_rating)
![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=vulnerabilities)
![Bugs](https://sonarcloud.io/api/project_badges/measure?project=FHICT-Kwetter_kwetter-identity&metric=bugs)

## Overview

This repo contains all the source code for the kwetter profile microservice.


## Packages
| Sample Name | Description |
| ----------- | ----------- |
| ProfileService.Api | This package contains the REST API specific code |
| ProfileService.Data | This package contains all the code for database communication |
| ProfileService.Domain | This package contains all the domain specific code |
| ProfileService.Messaging | This package contains all the pub/sub logic |
| ProfileService.Service | This package contains the use cases |

## Application Architecture

![Application Architecture](https://ik.imagekit.io/5ii0qakqx65/Kwetter_-__Code__-__C3__-__Profile_Service__cSS3JHGEkn.png)