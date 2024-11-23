﻿using Chronicle.Application.Interfaces;

namespace Chronicle.Application.Identity.Commands.UpdateProfile;

public record UpdateProfileCommand(string UserId, string FirstName, string LastName, string Email, string UserName, string PhoneNumber) : ICommandQuery;
