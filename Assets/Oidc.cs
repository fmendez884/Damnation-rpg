using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Oidc
{
    [JsonProperty("isLoadingUser")]
    public bool IsLoadingUser { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }
}

public partial class User
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_at")]
    public long ExpiresAt { get; set; }

    [JsonProperty("id_token")]
    public string IdToken { get; set; }

    [JsonProperty("profile")]
    public Profile Profile { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }

    [JsonProperty("session_state")]
    public string SessionState { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }
}

public partial class Profile
{
    [JsonProperty("aio")]
    public string Aio { get; set; }

    [JsonProperty("amr")]
    public string[] Amr { get; set; }

    [JsonProperty("ipaddr")]
    public string Ipaddr { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("oid")]
    public Guid Oid { get; set; }

    [JsonProperty("rh")]
    public string Rh { get; set; }

    [JsonProperty("sub")]
    public string Sub { get; set; }

    [JsonProperty("tid")]
    public Guid Tid { get; set; }

    [JsonProperty("unique_name")]
    public string UniqueName { get; set; }

    [JsonProperty("upn")]
    public string Upn { get; set; }

    [JsonProperty("uti")]
    public string Uti { get; set; }

    [JsonProperty("ver")]
    public string Ver { get; set; }

    [JsonProperty("__proto__")]
    public string Proto { get; set; }
}

