package com.latihanlks.pc_kab_klaten_joko_supriyanto.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;

import com.latihanlks.pc_kab_klaten_joko_supriyanto.R;

import org.w3c.dom.Text;

public class HomeFragment extends Fragment implements View.OnClickListener {

    private HomeViewModel homeViewModel;
    TextView txtHello, txtDate, txtNominal, txtKeuntungan, txtPresentase;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {

        homeViewModel = new ViewModelProvider(this).get(HomeViewModel.class);
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        return root;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        Button button = view.findViewById(R.id.btnLogout);
        button.setOnClickListener(v -> {
            Toast.makeText(getContext(), "Logout", Toast.LENGTH_SHORT).show();
            getActivity().finish();
        });
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()){
            case R.id.txtHello:

                break;
        }
    }
}