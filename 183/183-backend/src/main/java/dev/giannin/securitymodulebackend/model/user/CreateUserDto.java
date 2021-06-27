package dev.giannin.securitymodulebackend.model.user;

import lombok.Data;
import org.springframework.validation.annotation.Validated;

import javax.validation.constraints.NotNull;
import javax.validation.constraints.Pattern;
import javax.validation.constraints.Size;

@Data
@Validated
public class CreateUserDto {
    @NotNull
    @Size(min = 3, max = 100, message = "Your username must be between 3 and 100 characters long")
    private String username;
    @NotNull
    @Pattern(regexp = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{10,200}$", message = "Your password must be between 10 and 200 characters long, include at least one letter, one number and one special character.")
    private String password;
}
