package dev.giannin.securitymodulebackend.security.filter;

import dev.giannin.securitymodulebackend.service.UserService;
import dev.giannin.securitymodulebackend.util.JwtUtil;
import io.jsonwebtoken.SignatureException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.web.authentication.WebAuthenticationDetailsSource;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@Component
public class JwtRequestFilter extends OncePerRequestFilter {
    private JwtUtil jwtUtil;
    private UserService userService;

    @Autowired
    public JwtRequestFilter(JwtUtil jwtUtil, UserService userService){
        this.jwtUtil = jwtUtil;
        this.userService = userService;
    }

    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain) throws ServletException, IOException {
        final String authorizationHeader = request.getHeader("Authorization");

        String username = null;
        String jwt = null;

        if(authorizationHeader != null && authorizationHeader.startsWith("Bearer ")) {
            jwt = authorizationHeader.substring(7);

            try {
                username = jwtUtil.extractUsername(jwt);
            } catch (SignatureException e) {
                System.err.println("An invalid request to the resource " + request.getRequestURI() + " has been made. Additional Info" +
                        System.lineSeparator() + "Query: " + request.getQueryString() +
                        System.lineSeparator() + "Context Path:" + request.getContextPath());
            }
        }

        if (username != null && SecurityContextHolder.getContext().getAuthentication() == null) {

            UserDetails userDetails = this.userService.loadUserByUsername(username);

            if (jwtUtil.validateToken(jwt, userDetails)) {

                UsernamePasswordAuthenticationToken usernamePasswordAuthenticationToken = new UsernamePasswordAuthenticationToken(
                        userDetails, null, userDetails.getAuthorities());
                usernamePasswordAuthenticationToken
                        .setDetails(new WebAuthenticationDetailsSource().buildDetails(request));
                SecurityContextHolder.getContext().setAuthentication(usernamePasswordAuthenticationToken);

                System.out.println("A valid request has been made to the resource " + request.getMethod() + " " + request.getRequestURI() + " from the UserEntity " + jwtUtil.extractUsername(jwt) + " with possible other info: " +
                        System.lineSeparator() + "Query: " + request.getQueryString() +
                        System.lineSeparator() + "Context Path:" + request.getContextPath());
            }
        }
        filterChain.doFilter(request, response);
    }
}
