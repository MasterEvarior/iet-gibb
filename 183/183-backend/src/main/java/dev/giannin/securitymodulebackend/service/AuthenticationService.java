package dev.giannin.securitymodulebackend.service;

import dev.giannin.securitymodulebackend.model.user.AuthenticateUserDto;
import dev.giannin.securitymodulebackend.util.JwtUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

@Service
public class AuthenticationService {
    private JwtUtil jwtUtil;
    private UserService userService;
    private AuthenticationManager authManager;

    @Autowired
    public AuthenticationService(JwtUtil jwtUtil, UserService userService, AuthenticationManager authManager) {
        this.jwtUtil = jwtUtil;
        this.userService = userService;
        this.authManager = authManager;
    }

    public String authentication(AuthenticateUserDto dto){
        try {
            authManager.authenticate(
                    new UsernamePasswordAuthenticationToken(dto.getUsername(), dto.getPassword())
            );
            System.out.println("Valid login attempt has been made with the user " + dto.getUsername());
        } catch (BadCredentialsException e) {
            System.err.println("An invalid login attempt has been made with the username of " + dto.getUsername());
            throw new BadCredentialsException("Incorrect username or password");
        }

        final UserDetails userDetails = userService
                .loadUserByUsername(dto.getUsername());

        return jwtUtil.generateToken(userDetails);
    }

}
