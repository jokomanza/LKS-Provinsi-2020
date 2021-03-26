package com.latihanlks.pc_kab_klaten_joko_supriyanto;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;

public class DetailProductActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detail_product);

        ((Button)findViewById(R.id.btnBeli)).setOnClickListener( v -> {
            Intent intent = new Intent(this, BeliProduct.class);
            startActivity(intent);
        });
    }
}