package dev.giannin.securitymodulebackend.controller;

import dev.giannin.securitymodulebackend.model.user.AuthenticateUserDto;
import dev.giannin.securitymodulebackend.service.AuthenticationService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import javax.validation.Valid;

@RestController
public class LoginController {
    private AuthenticationService authenticationService;

    @Autowired
    public LoginController(AuthenticationService authenticationService) {
        this.authenticationService = authenticationService;
    }


    @PostMapping("/authenticate")
    public String authenticateUser(@RequestBody @Valid AuthenticateUserDto dto) {
        return authenticationService.authentication(dto);
    }
}

