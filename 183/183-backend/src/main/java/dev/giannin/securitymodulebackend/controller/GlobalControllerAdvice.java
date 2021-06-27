package dev.giannin.securitymodulebackend.controller;

import dev.giannin.securitymodulebackend.model.error.ErrorDto;
import org.springframework.core.Ordered;
import org.springframework.core.annotation.Order;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

import java.util.InputMismatchException;

@Order(Ordered.HIGHEST_PRECEDENCE)
@ControllerAdvice
public class GlobalControllerAdvice extends ResponseEntityExceptionHandler {

    @ExceptionHandler(Exception.class)
    public final ResponseEntity<ErrorDto> handleAllExceptions(final Exception e) {
        HttpStatus returnStatus = HttpStatus.INTERNAL_SERVER_ERROR;
        ErrorDto dto = new ErrorDto(returnStatus.value(), returnStatus.getReasonPhrase(), e.getMessage());
        return new ResponseEntity<>(dto, returnStatus);
    }

    @ExceptionHandler({IllegalArgumentException.class, BadCredentialsException.class, InputMismatchException.class})
    public final ResponseEntity<ErrorDto> handleIllegalArgumentExceptions(final Exception e) {
        HttpStatus returnStatus = HttpStatus.BAD_REQUEST;
        ErrorDto dto = new ErrorDto(returnStatus.value(), returnStatus.getReasonPhrase(), e.getMessage());
        return new ResponseEntity<>(dto, returnStatus);
    }

}
