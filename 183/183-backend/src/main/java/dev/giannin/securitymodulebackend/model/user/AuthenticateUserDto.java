package dev.giannin.securitymodulebackend.model.user;

import lombok.Data;
import org.springframework.validation.annotation.Validated;

import javax.validation.constraints.NotEmpty;

@Data
@Validated
public class AuthenticateUserDto {
    @NotEmpty
    private final String username;
    @NotEmpty
    private final String password;
}
