	- Se ha creado un servicio con su interfaz ITokenService en el que se recupera un token JWT de esta manera:
	JwtSecurityTokenHandler tokenHandler = new();
	byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
	SecurityTokenDescriptor tokenDescriptor = new()
		{
		    Subject = new ClaimsIdentity(new Claim[]
		    {
		        new(ClaimTypes.Name, user.Name),
		        new(ClaimTypes.Email, user.email)
	    }),
	    Expires = DateTime.UtcNow.AddHours(1),
	    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),           SecurityAlgorithms.HmacSha256Signature),
	    Issuer = _configuration["Jwt:Issuer"],
	    Audience = _configuration["Jwt:Audience"]
	};
	SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
	return tokenHandler.WriteToken(token);

	- Esto se hace para describir el tipo de token que queremos devolver que también lo especificamos a través del appsettings ->
  "Jwt": {
	  "Issuer": "WebApiJwt.com",
	  "Audience": "localhost",
	  "Key": "S3cr3t_K3y!.123_S3cr3t_K3y!.123@#A7((!?67310¡"
   }

	- Por último, especificaremos el tipo de token que vamos a recuperar en el           program.cs
	builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


