# Frends.Community.UDP

frends Community Task for UdpTasks

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.UDP/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.UDP/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.UDP) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [SendAndReceive](#UdpTasks)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the Task via frends UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.UDP

# Tasks

## UdpTasks

Sends an udp message and waits for a reply

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Commands | `Array{Send command:string, Response start: string, Response end:string` | Arrauy of : command to send to receiver and start and end part of response. The task enforces start and end tags, so if none is found the task will eventually timeout  | `Send command: "MODEL\r, Response start: ACK, Response end: "\r"` |
| Ip address | `string` | IP address of endpoint | `10.0.0.1` |
| Port | `string` | IP port of endpoint | `10.0.0.1` |

### Options

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Timeout | `int` | Timeout for operation in milliseconds | `3000` |

### Returns

A result object with parameters.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Responses | `Array {string}` | String array of responses. Item count in array is equal to command counf and the order is the same | `ACK MODEL unD6IO-BT\r, ACK VERSION 1.6\r, ACK BTS 0\r` |

Example response:
```
{
	"Responses": [
		"ACK MODEL unD6IO-BT\r",
		"ACK VERSION 1.6\r",
		"ACK BTS 0\r"
	]
}
```

Usage:
To fetch result use syntax:

`#result.Responses[0]`

# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.UDP.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ------- | ------- |
| 1.0.0   | First version |
